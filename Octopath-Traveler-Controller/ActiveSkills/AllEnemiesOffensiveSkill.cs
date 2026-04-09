using Octopath_Traveler_Model;
namespace Octopath_Traveler.ActiveSkills;

public class AllEnemiesOffensiveSkill : OffensiveSkillEffect
{
    public AllEnemiesOffensiveSkill(Skill skill) : base(skill) { }

    protected override List<Beast> SelectVictims(List<Beast> enemyTeam)
        => enemyTeam.Where(e => !e.IsDead).ToList();
}