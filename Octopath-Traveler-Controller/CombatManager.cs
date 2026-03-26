using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

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
        var turnManager = new TurnManager(_playerTeam, _enemyTeam);
        List<Unit> currentTurns = turnManager.GetCurrentRoundTurns();
        List<Unit> nextTurns = turnManager.GetNextRoundTurns();
        for (int i = 0; i < currentTurns.Count; i++)
        {
            Unit unit = currentTurns[i];
            if (unit.IsDead) continue; 
            if (!IsBattleActive()) break;
            List<Unit> remainingTurns = currentTurns.Skip(i).Where(u => !u.IsDead).ToList();
            List<Unit> aliveNextTurns = nextTurns.Where(u => !u.IsDead).ToList();
            ShowCurrentGameState(remainingTurns, aliveNextTurns, turnManager);
            ProcessUnitTurn(unit);
        }
    }
    private void ShowCurrentGameState(List<Unit> remainingTurns, List<Unit> nextTurns, TurnManager turnManager)
    {
        var gameStateManager = new StateManager(_view, _playerTeam, _enemyTeam);
        gameStateManager.GameStateStatsMessage();
        _view.ShowTurnsMessage(turnManager.GetTurnNames(remainingTurns), turnManager.GetTurnNames(nextTurns));
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
        bool turnCompleted = false;
        while (!turnCompleted) 
        {
            _view.ShowOptionsTavelerMessage(traveler.Name, traveler.Optionsattack);
            string choice = _view.ReadLine();
            ICombatAction action;
            switch (choice)
            {
                case "1":
                    action = new BasicAttackAction();
                    break;
                case "2":
                    action = new UseSkillAction();
                    break;
                case "3":
                    action = new DefendAction();
                    break;
                case "4":
                    action = new RunAwayAction();
                    break;
                default:
                    action = new BasicAttackAction();
                    break;
            }
            turnCompleted = action.Execute(traveler, _playerTeam, _enemyTeam, _view);
            if (action is RunAwayAction)
            {
                _travelersRunAway = true;
            }
        }
    }
    private void HandleEnemyTurn(Beast beast)
    {
        var beastAction = new EnemyTurn(beast, _playerTeam, _view);
        beastAction.ExecuteEnemyActionBasedOnSkill();
    }
    private void ShowFinalResult()
    {
        if (_playerTeam.Any(t => t.CurrentHp > 0) && !_travelersRunAway)
        {
            _view.ShowVictoryTravelerMessage();
        }
        else
        {
            _view.ShowDefetedTravelerMessage();
        }
    }
    // sacar de aca esto quedo solo por la entrega 1
    private void GainedBoostPoints()
    {
        foreach (var traveler in _playerTeam.Where(t => !t.IsDead))
        {
            if (traveler.CurrentBp < 5) 
            {
                traveler.CurrentBp++;
            }
        }
    }
}