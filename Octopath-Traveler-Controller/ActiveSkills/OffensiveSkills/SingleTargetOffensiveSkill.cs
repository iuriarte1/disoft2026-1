using Octopath_Traveler_Model;
namespace Octopath_Traveler.ActiveSkills;

public class SingleTargetOffensiveSkill : OffensiveSkillEffect
{
    private readonly Beast _victim;
    public SingleTargetOffensiveSkill(Skill skill, Beast victim, bool showSkillUsed = true, bool showFinalHp = true) : base(skill, showSkillUsed, showFinalHp)
    {
        _victim = victim;
    }

    protected override List<Beast> SelectVictims(List<Beast> enemyTeam)
    {
        return new List<Beast>{ _victim };
    }
}