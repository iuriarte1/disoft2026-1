using Octopath_Traveler_Model;
using Octopath_Traveler;

public static class ActiveSkillDamageCalculator
{
    public static (int damage, bool enteredBreakingPoint) Calculate(Traveler actor, Beast victim, Skill skill)
        => DamageCalculator.Calculate(actor, victim, skill.Type, skill.Modifier);

    // CalculateRawDamageAndShieldReduction se mantiene para LastStand que lo usa directamente
    public static (double rawDamage, bool enteredBreakingPoint) CalculateRawDamageAndShieldReduction(
        Traveler actor, Beast victim, Skill skill)
    {
        double baseDamage = DamageCalculator.GetBaseDamage(actor, victim, skill.Type, skill.Modifier);
        bool enteredBreakingPoint = ShieldManager.TryReduceShield(victim, skill.Type, baseDamage);
        double multiplier = DamageCalculator.GetDamageMultiplier(victim, skill.Type);
        return (baseDamage * multiplier, enteredBreakingPoint);
    }
}
