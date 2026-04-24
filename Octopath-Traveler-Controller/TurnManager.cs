using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class TurnManager
{
    private readonly List<Traveler> _players;
    private readonly List<Beast> _enemies;
    private readonly List<Unit> _allCombatants;

    public TurnManager(List<Traveler> players, List<Beast> enemies)
    {
        _players = players;
        _enemies = enemies;
        _allCombatants = players.Concat(enemies.Cast<Unit>()).ToList();
    }

    public List<Unit> GetCurrentRoundTurns()
        => BuildQueue(
            filter: CanActThisRound,
            categorySelector: u => u.GetCategoryForCurrentRound());

    public List<Unit> GetNextRoundTurns()
        => BuildQueue(
            filter: WillActNextRound,
            categorySelector: u => u.GetCategoryForNextRound());

    public List<string> GetTurnNames(List<Unit> turnList)
        => turnList.Select(u => u.Name).ToList();

    private List<Unit> BuildQueue(
        Func<Unit, bool> filter,
        Func<Unit, TurnPriorityCategory> categorySelector)
    {
        return _allCombatants
            .Where(filter)
            .OrderBy(categorySelector)
            .ThenByDescending(u => u.BaseStats.Speed)
            .ThenBy(GetTravelerPriority)
            .ThenBy(GetBoardIndex)
            .ToList();
    }

    private bool CanActThisRound(Unit unit)
        => !unit.IsDead && !IsInBreakingPoint(unit);

    private bool WillActNextRound(Unit unit)
        => !unit.IsDead && (!IsInBreakingPoint(unit) || IsExitingBreakingPoint(unit));

    private bool IsInBreakingPoint(Unit unit)
        => unit is Beast beast && beast.IsInBreakingPoint;

    private bool IsExitingBreakingPoint(Unit unit)
        => unit is Beast beast && beast.RoundsInBreakingPoint == 1;

    private int GetTravelerPriority(Unit unit)
        => unit is Traveler ? 0 : 1;

    private int GetBoardIndex(Unit unit)
    {
        if (unit is Traveler t) return _players.IndexOf(t);
        if (unit is Beast b)    return _enemies.IndexOf(b);
        return int.MaxValue;
    }
}