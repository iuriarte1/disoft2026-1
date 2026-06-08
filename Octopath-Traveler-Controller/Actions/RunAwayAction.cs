using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.PassiveSkills;

namespace Octopath_Traveler.Actions;

public class RunAwayAction : ICombatAction
{
    public bool Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view, PassiveSkillManager passiveManager)
    {
        view.ShowRunAwayMessage();
        return true;
    }
}