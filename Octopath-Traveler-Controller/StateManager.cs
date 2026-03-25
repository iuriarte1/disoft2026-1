using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler;

public class StateManager
{
    private View _view;
    private TeamManager _teamManager;
    private TeamManager _enemyManager;
    public StateManager(View view, List<Traveler> teamManager, List<Beast> enemyManager)
    {
        _view = view;
        _teamManager = new TeamManager(teamManager.Cast<Unit>().ToList());
        _enemyManager = new TeamManager(enemyManager.Cast<Unit>().ToList());
    }

    public void GameStateStatsMessage()
    {
        List<string> gameState = new List<string> {"Equipo del jugador"};
        gameState.AddRange(_teamManager.GetTeamCurrentStats());
        gameState.Add("Equipo del enemigo");
        gameState.AddRange(_enemyManager.GetTeamCurrentStats());
        _view.ShowGameStateTeams(gameState);
    }
    
}