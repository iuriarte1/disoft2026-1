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
            .OrderByDescending(u => u.BaseStats.Speed) 
            .ToList();
    }
    public List<string> GetTurnNames(List<Unit> turnList)
    {
        return turnList.Select(u => u.Name).ToList();
    }
}