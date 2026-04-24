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
        _allCombatants = players.Cast<Unit>().Concat(enemies.Cast<Unit>()).ToList();
    }

    public List<Unit> GetCurrentRoundTurns()
    {
        return _allCombatants
            .Where(u => !u.IsDead && !IsInBreakingPoint(u))
            .OrderBy(GetCurrentCategory)
            .ThenByDescending(u => u.BaseStats.Speed)
            .ThenBy(GetTravelerPriority)
            .ThenBy(GetBoardIndex)
            .ToList();
    }

    public List<Unit> GetNextRoundTurns()
    {
        return _allCombatants
            .Where(u => !u.IsDead && (!IsInBreakingPoint(u) || IsExitingBreakingPoint(u)))
            .OrderBy(GetNextCategory)
            .ThenByDescending(u => u.BaseStats.Speed)
            .ThenBy(GetTravelerPriority)
            .ThenBy(GetBoardIndex)
            .ToList();
    }
    private bool IsExitingBreakingPoint(Unit unit)
        => unit is Beast beast && beast.IsInBreakingPoint && beast.RoundsInBreakingPoint == 1;
    public List<string> GetTurnNames(List<Unit> turnList)
        => turnList.Select(u => u.Name).ToList();

    // Categorías para la ronda ACTUAL
    // Cat 0: defender (ronda anterior) → ya actúan en la ronda actual con prioridad
    // Cat 1: prioridad por habilidad
    // Cat 2: normal
    // Cat 3: despriorizado (Leghold Trap)
    private int GetCurrentCategory(Unit unit)
    {
        if (unit is Beast b && b.JustRecoveredFromBreakingPoint) return 0; // ← cat 0
        if (unit is Traveler t && t.HasDefendPriorityThisRound) return 1;
        if (unit.HasTurnPriorityThisRound) return 2;
        if (unit is Beast b2 && b2.RoundsInLastTurn > 0) return 4;
        return 3;
    }

    // Categorías para la ronda SIGUIENTE
    // Cat 0: bestias saliendo de Breaking Point
    // Cat 1: defender
    // Cat 2: prioridad por habilidad
    // Cat 3: normal
    // Cat 4: despriorizado
    private int GetNextCategory(Unit unit)
    {
        if (unit is Beast b && b.RoundsInBreakingPoint == 1) return 0; // ← cat 0: sale de BP
        if (unit is Traveler t && t.HasDefendPriorityNextRound) return 1;
        if (unit.HasTurnPriorityFromSkill) return 2;
        if (unit is Beast b2 && b2.RoundsInLastTurn > 1) return 4;
        return 3;
    }

    // Viajeros antes que bestias en empate
    private int GetTravelerPriority(Unit unit)
        => unit is Traveler ? 0 : 1;

    // Posición en el tablero
    private int GetBoardIndex(Unit unit)
    {
        if (unit is Traveler t) return _players.IndexOf(t);
        if (unit is Beast b) return _enemies.IndexOf(b);
        return int.MaxValue;
    }
    private bool IsInBreakingPoint(Unit unit)
        => unit is Beast beast && beast.IsInBreakingPoint;
}