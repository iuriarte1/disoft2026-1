using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class DefendAction : ICombatAction
{
    public bool Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        actor.IsDefendingThisRound = true;
        actor.UsedDefender = true;
        return true;
    }
}