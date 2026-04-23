using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public interface IBeastSkillEffect
{
    void Execute(Beast actor, List<Traveler> playerTeam, View view);
}