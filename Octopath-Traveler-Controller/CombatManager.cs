using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;
using Octopath_Traveler.Combat;
using Octopath_Traveler.EnemyCombat;
using Octopath_Traveler.PassiveSkills;

namespace Octopath_Traveler;

public class CombatManager
{
    private readonly View _view;
    private readonly List<Traveler> _playerTeam;
    private readonly List<Beast> _enemyTeam;
    private int _roundCount = 1;
    private bool _travelersRunAway = false;
    public CombatManager(View view, List<Traveler> playerTeam, List<Beast> enemyTeam)
    {
        _view = view;
        _playerTeam = playerTeam;
        _enemyTeam = enemyTeam;
    }
    public void StartBattle()
    {
        var passiveManager = new PassiveSkillManager(_playerTeam);
        passiveManager.ApplyBattleStartEffects();
        while (IsBattleActive())
        {
            _view.ShowRoundMessage(_roundCount);
            ExecuteRound();
            GainedBoostPoints();
            _roundCount++;
        }
        ShowFinalResult();
    }
    private bool IsBattleActive()
    {
        if (_travelersRunAway) return false;
        return _playerTeam.Any(t => t.CurrentHp > 0) && _enemyTeam.Any(b => b.CurrentHp > 0);
    }
    private void ExecuteRound()
    {
        foreach (var traveler in _playerTeam)
            traveler.PrepareForNewRound();
        var turnManager = new TurnManager(_playerTeam, _enemyTeam);
        var alreadyActed = new HashSet<Unit>();
        
        List<Unit> nextTurns = turnManager.GetNextRoundTurns();
        while (true)
        {
            List<Unit> currentTurns = turnManager.GetCurrentRoundTurns()
                .Where(u => !alreadyActed.Contains(u) && !u.RevivedThisRound)
                .ToList();

            if (!currentTurns.Any() || !IsBattleActive()) break;

            Unit unit = currentTurns.First();
            if (unit.IsDead) { alreadyActed.Add(unit); continue; }

            ShowCurrentGameState(currentTurns, nextTurns, turnManager);
            ProcessUnitTurn(unit);
            alreadyActed.Add(unit);
            nextTurns = turnManager.GetNextRoundTurns();
        }

        EndOfRoundCleanup();
    }
    private void ShowCurrentGameState(List<Unit> remainingTurns, List<Unit> nextTurns, TurnManager turnManager)
    {
        var gameStateManager = new StateManager(_view, _playerTeam, _enemyTeam);
        gameStateManager.GameStateStatsMessage();
        _view.ShowTurnsMessage(turnManager.GetNames(remainingTurns), turnManager.GetNames(nextTurns));
    }
    private void ProcessUnitTurn(Unit unit)
    {
        if (unit is Traveler traveler)
        {
            HandlePlayerTurn(traveler);
        }
        else if (unit is Beast beast)
        {
            HandleEnemyTurn(beast);
        }
    }
    private void HandlePlayerTurn(Traveler traveler)
    {
        var playerHandler =new PlayerTurnHandler(_view, _playerTeam, _enemyTeam);
        playerHandler.ExecuteTurn(traveler);
        if (playerHandler.ranAway) 
            _travelersRunAway = true;
    }
    private void HandleEnemyTurn(Beast beast)
    {
        var beastAction = new EnemyTurn(beast, _playerTeam, _view);
        beastAction.Execute();
    }
    
    private void ShowFinalResult()
    {
        if (_playerTeam.Any(t => !t.IsDead) && !_travelersRunAway)
            _view.ShowVictoryTravelerMessage();
        else
            _view.ShowDefetedTravelerMessage();
    }
    private void GainedBoostPoints()
    {
        foreach (var traveler in _playerTeam.Where(t => !t.IsDead))
        {
            traveler.GainBp();
        }
    }
    private void EndOfRoundCleanup()
    {
        var clean = new EndOfRoundCleaner(_playerTeam, _enemyTeam);
        clean.Clean();
    }

}