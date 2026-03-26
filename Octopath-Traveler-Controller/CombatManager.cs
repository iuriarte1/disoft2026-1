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
            AwardBoostPoints();
            _roundCount++;
        }

        DisplayFinalResult();
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
        bool turnCompleted = false; // Bandera para saber si logró actuar

        // Bucle que atrapa las cancelaciones
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

            // Aquí ejecutamos la acción. 
            // Si el jugador cancela, action.Execute devuelve 'false', 
            // turnCompleted sigue siendo 'false', y el while vuelve a empezar el menú.
            // Si el jugador ataca, devuelve 'true' y rompemos el while.
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
        beastAction.Execute();
    }
    private void DisplayFinalResult()
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
    private void AwardBoostPoints()
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