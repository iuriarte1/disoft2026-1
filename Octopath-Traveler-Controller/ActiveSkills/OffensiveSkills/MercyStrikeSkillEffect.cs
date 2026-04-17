using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class MercyStrikeSkillEffect : SingleTargetOffensiveSkill
{
    private Beast _victim;

    public MercyStrikeSkillEffect(Skill skill, Beast victim) : base(skill, victim)
    {
        _victim = victim;
    }

    protected override (int damage, bool enteredBreakingPoint) CalculateDamage(Traveler atacante, Beast victim)
    {
        var (damage, enteredBreakingPoint) = base.CalculateDamage(atacante, victim);
        int limitedDamage = Math.Min(damage,_victim.CurrentHp - 1);
        return (Math.Max(limitedDamage, 0), enteredBreakingPoint);
    }
}