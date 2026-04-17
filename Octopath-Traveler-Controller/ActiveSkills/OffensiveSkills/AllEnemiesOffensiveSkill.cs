using Octopath_Traveler_Model;
namespace Octopath_Traveler.ActiveSkills;

public class AllEnemiesOffensiveSkill : OffensiveSkillEffect
{
    public AllEnemiesOffensiveSkill(Skill skill, bool showSkillUsed = true, bool showFinalHp = true) : base(skill, showSkillUsed, showFinalHp) { }

    protected override List<Beast> SelectVictims(List<Beast> enemyTeam)
        => enemyTeam.Where(e => !e.IsDead).ToList();
}