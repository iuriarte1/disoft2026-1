using Octopath_Traveler_Model;
using Octopath_Traveler_View;
namespace Octopath_Traveler.ActiveSkills;

public interface IActiveSkillEffect
{
    void Execute(Traveler actor, List<Traveler> playerTeam, 
        List<Beast> enemyTeam, View view);

}