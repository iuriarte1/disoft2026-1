using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public interface ICombatAction
{
    bool Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view);
}