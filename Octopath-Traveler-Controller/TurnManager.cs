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
    {
        return _allCombatants
            .Where(CanActThisRound)
            .OrderBy(u => u.GetCategoryForCurrentRound())
            .ThenByDescending(u => u.BaseStats.Speed)
            .ThenBy(IsPlayer)
            .ThenBy(BoardIndex)
            .ToList();
    }

    public List<Unit> GetNextRoundTurns()
    {
        return _allCombatants
            .Where(WillActNextRound)
            .OrderBy(u => u.GetCategoryForNextRound())
            .ThenByDescending(u => u.BaseStats.Speed)
            .ThenBy(IsPlayer)
            .ThenBy(BoardIndex)
            .ToList();
    }

    public List<string> GetNames(List<Unit> turns)
        => turns.Select(u => u.Name).ToList();

    private bool CanActThisRound(Unit unit)
        => !unit.IsDead && !IsInBreakingPoint(unit);

    private bool WillActNextRound(Unit unit)
        => !unit.IsDead && (!IsInBreakingPoint(unit) || IsLeavingBreakingPoint(unit));

    private bool IsInBreakingPoint(Unit unit)
        => unit is Beast beast && beast.IsInBreakingPoint;

    private bool IsLeavingBreakingPoint(Unit unit)
        => unit is Beast beast && beast.RoundsInBreakingPoint == 1;

    private int IsPlayer(Unit unit)
        => unit is Traveler ? 0 : 1;

    private int BoardIndex(Unit unit)
    {
        if (unit is Traveler t) return _players.IndexOf(t);
        if (unit is Beast b) return _enemies.IndexOf(b);
        return int.MaxValue;
    }
}