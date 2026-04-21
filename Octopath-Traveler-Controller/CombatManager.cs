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
        // copiar HasTurnPriorityFromSkill → HasTurnPriorityThisRound
        foreach (var traveler in _playerTeam)
            traveler.HasTurnPriorityThisRound = traveler.HasTurnPriorityFromSkill;

        var turnManager = new TurnManager(_playerTeam, _enemyTeam);
        var alreadyActed = new HashSet<Unit>();
        

        // resetear HasTurnPriorityFromSkill ya que fue copiado
        foreach (var traveler in _playerTeam)
            traveler.HasTurnPriorityFromSkill = false;
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
    private void EndOfRoundCleanup()
    {
        foreach (var traveler in _playerTeam)
        {
            traveler.UsedDefender = false;
            traveler.HasTurnPriorityThisRound = false;
            traveler.RevivedThisRound = false;
        }
        foreach (var beast in _enemyTeam.Where(b => b.RoundsInLastTurn > 0))
            beast.RoundsInLastTurn--;
    }

}