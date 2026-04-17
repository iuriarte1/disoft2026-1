using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class TurnManager
{
    private readonly List<Unit> _allCombatants;

    public TurnManager(List<Traveler> players, List<Beast> enemies)
    {
        _allCombatants = players.Cast<Unit>().Concat(enemies.Cast<Unit>()).ToList();
    }
    public List<Unit> GetCurrentRoundTurns()
    {
        return _allCombatants
            .Where(u => !u.IsDead)
            .OrderByDescending(u => u.BaseStats.Speed)
            .ToList();
    }
    public List<Unit> GetNextRoundTurns()
    {
        return _allCombatants
            .Where(u => !u.IsDead)
            .OrderBy(GetTurnCategory)
            .ThenByDescending(u => u.BaseStats.Speed)
            .ToList();
    }
    public List<string> GetTurnNames(List<Unit> turnList)
    {
        return turnList.Select(u => u.Name).ToList();
    }
    private int GetTurnCategory(Unit unit)
    {
        if (unit is Beast beast && beast.IsInBreakingPoint) return 0;
        if (unit.HasTurnPriorityFromSkillOrDef) return 1;
        if (unit is Beast b && b.RoundsInLastTurn > 0) return 3;
        return 2;
    }
}