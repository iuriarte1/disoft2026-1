using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills.BeastSelectionSkill;

public class AutoSelectSingleTargetSkill : OffensiveSkillEffect
{
    private readonly Func<List<Beast>, Beast> _selector;

    public AutoSelectSingleTargetSkill(Skill skill, Func<List<Beast>, Beast> selector) : base(skill)
    {
        _selector = selector;
    }

    protected override List<Beast> SelectVictims(List<Beast> enemyTeam)
    {
        var living = enemyTeam.Where(e => !e.IsDead).ToList();
        return new List<Beast> { _selector(living) };
    }
}