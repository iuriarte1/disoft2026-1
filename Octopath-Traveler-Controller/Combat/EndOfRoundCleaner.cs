using Octopath_Traveler_Model;

namespace Octopath_Traveler.Combat;

public class EndOfRoundCleaner
{
    private readonly List<Traveler> _playerTeam;
    private readonly List<Beast> _enemyTeam;

    public EndOfRoundCleaner(List<Traveler> playerTeam, List<Beast> enemyTeam)
    {
        _playerTeam = playerTeam;
        _enemyTeam = enemyTeam;
    }

    public void Clean()
    {
        CleanPlayerTeam();
        CleanEnemyTeam();
    }

    private void CleanPlayerTeam()
    {
        foreach (var traveler in _playerTeam)
            traveler.EndOfRoundCleanUp();
    }

    private void CleanEnemyTeam()
    {
        ResetBreakingPointRecoveryFlags();
        TickTurnDelays();
        TickBreakingPointCounters();
    }

    private void ResetBreakingPointRecoveryFlags()
    {
        foreach (var beast in _enemyTeam)
            beast.ClearBreakingPointRecovery();
    }

    private void TickTurnDelays()
    {
        foreach (var beast in _enemyTeam.Where(b => b.RoundsInLastTurn > 0))
            beast.TickTurnDelay();
    }

    private void TickBreakingPointCounters()
    {
        foreach (var beast in _enemyTeam.Where(b => b.IsInBreakingPoint))
            beast.TickBreakingPoint();
    }
}