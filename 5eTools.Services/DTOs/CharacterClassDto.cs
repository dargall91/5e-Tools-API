namespace _5eTools.Services.DTOs;

public class CharacterClassDto
{
    public int Level { get; set; }

    public int HitDiceUsed { get; set; }

    public bool BaseClass { get; set; }

    public int ClassSaveDc { get; set; }

    public int SubclassId { get; set; }

    public PrimalCompanionDto? PrimalCompanionDto { get; set; }
}
