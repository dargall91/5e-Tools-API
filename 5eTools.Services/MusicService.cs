using _5eTools.Data;
using _5eTools.Data.Entities;
using _5eTools.Services.DTOs;
using Microsoft.Extensions.Configuration;
using SoundFlow.Abstracts;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Enums;
using SoundFlow.Providers;

namespace _5eTools.Services;

public interface IMusicService
{
    bool MusicIdExists(int id);
    List<string> ValidateMusicDto(MusicDto musicDto);
    void Play(int id);
    void Pause();
    void Stop();
    List<string> FindNewMusic();
    int Add(MusicDto musicDto);
}

public class MusicService(ToolsDbContext dbContext, IConfiguration configuration) : IMusicService
{
    private readonly string musicFolder = configuration["MusicFolder"]!;
    //unused, but must be initalized here for the SoundPlayer to work. Service lifetime must be properly be properly set so it can be disposed
    private static readonly AudioEngine audioEngine = new MiniAudioEngine(44100, Capability.Playback);
    private static SoundPlayer? SoundPlayer { get; set; }
    private static string? CurrentlyPlaying { get; set; }

    public bool MusicIdExists(int id) => dbContext.Music.Find(id) != default;

    public List<string> ValidateMusicDto(MusicDto musicDto)
    {
        var errors = new List<string>();

        if (dbContext.Music.Any(x => x.Name == musicDto.Name))
        {
            errors.Add($"A track with the name {musicDto.Name} already exists.");
        }

        if (dbContext.Music.Any(x => x.FileName == musicDto.FileName))
        {
            errors.Add($"A track for the file {musicDto.FileName} already exists.");
        }

        if (musicDto.LoopStartTime > musicDto.LoopEndTime)
        {
            errors.Add("Loop End Time must be less than or equal to Loop Start Time");
        }

        return errors;
    }

    public void Play(int id)
    {
        var track = dbContext.Music.Find(id)!;

        if (SoundPlayer == null)
        {
            Play(track);
        }
        else if (CurrentlyPlaying != track.Name)
        {
            SoundPlayer.Stop();
            Mixer.Master.RemoveComponent(SoundPlayer);
            Play(track);
        }
        else
        {
            //Selected track is already loaded
            SoundPlayer.Play();
        }
    }

    public void Pause()
    {
        if (SoundPlayer != default)
        {
            SoundPlayer.Pause();
        }
    }

    public void Stop()
    {
        if (SoundPlayer != default)
        {
            SoundPlayer.Stop();
        }
    }

    public List<string> FindNewMusic()
    {
        var trackList = dbContext.Music.Select(x => x.FileName).ToList();

        var newTracks = Directory.GetFiles(musicFolder)
            .Select(x => Path.GetFileName(x))
            .Where(x => !trackList.Contains(x));

        return newTracks.ToList();
    }

    public int Add(MusicDto musicDto)
    {
        var newMusic = new Music
        {
            Name = musicDto.Name,
            FileName = musicDto.FileName,
            LoopStartTime = musicDto.LoopStartTime,
            LoopEndTime = musicDto.LoopEndTime
        };

        dbContext.Add(newMusic);
        dbContext.SaveChanges();

        return newMusic.Id;
    }

    public List<ListItem> FindAll()
    {
        return dbContext.Music
            .Select(x => new ListItem
            {
                Id = x.Id,
                Name = x.Name
            })
            .OrderBy(x => x.Name)
            .ToList();
    }
    private void Play(Music music)
    {
        CurrentlyPlaying = music.Name;
        var dataProvider = new ChunkedDataProvider(musicFolder + music.FileName);

        SoundPlayer = new SoundPlayer(dataProvider)
        {
            IsLooping = true
        };
        SoundPlayer.SetLoopPoints(music.LoopStartTime, music.LoopEndTime);

        Mixer.Master.AddComponent(SoundPlayer);
        SoundPlayer.Play();
    }
}
