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
        double baseDamage = DamageCalculator.GetBaseDamage(atacante, victim, _skill.Type, _skill.Modifier);
        double multiplier = DamageCalculator.GetDamageMultiplier(victim, _skill.Type);
        int rawDamage = Convert.ToInt32(Math.Floor(baseDamage * multiplier));
    
        int limitedDamage = Math.Max(Math.Min(rawDamage, victim.CurrentHp - 1), 0);
    
        bool enteredBreakingPoint = ShieldManager.TryReduceShield(victim, _skill.Type, limitedDamage);
    
        return (limitedDamage, enteredBreakingPoint);
    }
}