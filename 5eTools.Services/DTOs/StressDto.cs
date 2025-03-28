namespace _5eTools.Services.DTOs;

public class StressDto
{
    public int StressLevel { get; set; }

    public int StressThreshold { get; set; }

    public int StressMaximum { get; set; }

    public int MeditationDiceUsed { get; set; }

    public int? StresStatusId { get; set; }
}
