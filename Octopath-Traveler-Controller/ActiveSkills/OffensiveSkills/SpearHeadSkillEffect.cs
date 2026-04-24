using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class SpearHeadSkillEffect : SingleTargetOffensiveSkill
{
    private Traveler _user;

    public SpearHeadSkillEffect(Skill skill, Beast victim, Traveler user) : base(skill, victim)
    {
        _user = user;
    }

    public override void Execute(Traveler user, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        base.Execute(user, playerTeam, enemyTeam, view);
        _user.GrantTurnPriority();
    }
}