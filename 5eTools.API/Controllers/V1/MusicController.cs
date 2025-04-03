using _5eTools.API.Models;
using _5eTools.Services;
using _5eTools.Services.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace _5eTools.API.Controllers.V1;

[ApiController]
[Route("api/v{version:apiversion}/[controller]")]
public class MusicController(IMusicService musicService) : ControllerBase
{
    [HttpPost("play/{id}")]
    public IActionResult Play(int id)
    {
        if (musicService.MusicIdExists(id))
        {
            musicService.Play(id);

            return Ok(new ResponseWrapper<bool>(true));
        }

        return NotFound(new ResponseWrapper<bool>($"No music with ID {id} found."));
    }

    [HttpPost("pause")]
    public IActionResult Pause()
    {
        musicService.Pause();

        return Ok(new ResponseWrapper<bool>(true));
    }

    [HttpPost("stop")]
    public IActionResult Stop()
    {
        musicService.Stop();

        return Ok(new ResponseWrapper<bool>(true));
    }

    [HttpGet]
    public IActionResult GetNewTracks()
    {
        return Ok(new ResponseWrapper<List<string>>(musicService.FindNewMusic()));
    }

    [HttpPut]
    public IActionResult Add(MusicDto musicDto)
    {
        var errors = musicService.ValidateMusicDto(musicDto);
        if (errors.Count > 0)
        {
            return BadRequest(new ResponseWrapper<bool>(errors));
        }

        var id = musicService.Add(musicDto);

        return CreatedAtAction(nameof(Play), new { id }, musicDto);
    }
}
