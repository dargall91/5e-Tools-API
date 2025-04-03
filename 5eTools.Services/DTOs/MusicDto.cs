namespace _5eTools.Services.DTOs;

public class MusicDto
{
    public required string Name { get; set; }

    public required string FileName { get; set; }

    public required float LoopStartTime { get; set; }

    public required float LoopEndTime { get; set; }
}
