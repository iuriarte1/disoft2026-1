using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.Combat;

public class PlayerTurnHandler
{
    private readonly View _view;
    private readonly List<Traveler> _playerTeam;
    private readonly List<Beast> _enemyTeam;
    public bool ranAway = false;
    

    public PlayerTurnHandler(View view, List<Traveler> playerTeam, List<Beast> enemyTeam)
    {
        _view = view;
        _playerTeam = playerTeam;
        _enemyTeam = enemyTeam;
    }

    public void ExecuteTurn(Traveler traveler)
    {
        bool turnCompleted = false;
        while (!turnCompleted)
        {
            ICombatAction action = AskPlayerForAction(traveler);
            turnCompleted = action.Execute(traveler, _playerTeam, _enemyTeam, _view);
            UpdateRunAwayState(action);
        }
    }

    private ICombatAction AskPlayerForAction(Traveler traveler)
    {
        _view.ShowOptionsTavelerMessage(traveler.Name, traveler.ActionOptions);
        string choice = _view.ReadLine();
        return ActionFactory.Create(choice);
    }

    private void UpdateRunAwayState(ICombatAction action)
    {
        if (action is RunAwayAction)
            ranAway = true;
    }
}