using _5eTools.Services.DTOs;

namespace _5eTools.Services;

public interface ICombatantService
{
    List<CombatantDto> GetCombatants();
    void SetCombatantList(IEnumerable<CombatantDto> combatants);
    void ClearCombatants();
}

public class CombatantService : ICombatantService
{
    private static List<CombatantDto> CombatantList { get; } = new List<CombatantDto>();

    public List<CombatantDto> GetCombatants() => CombatantList;

    public void SetCombatantList(IEnumerable<CombatantDto> combatants)
    {
        CombatantList.Clear();
        CombatantList.AddRange(combatants.OrderBy(x => x.Order));
    }

    public void ClearCombatants()
    {
        CombatantList.Clear();
    }
}
