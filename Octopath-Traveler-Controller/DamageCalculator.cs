using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public static class DamageCalculator
{
    private static readonly string[] PhysicalTypes =
        { "Sword", "Spear", "Axe", "Dagger", "Bow", "Stave" };

    public static bool IsPhysical(string attackType)
        => PhysicalTypes.Contains(attackType);

    public static double GetBaseDamage(Traveler actor, Beast victim, string attackType, double modifier)
    {
        return IsPhysical(attackType)
            ? actor.BaseStats.PhysicalAttack * modifier - victim.BaseStats.PhysicalDefense
            : actor.BaseStats.ElementalAttack * modifier - victim.BaseStats.ElementalDefense;
    }

    public static double GetDamageMultiplier(Beast victim, string attackType)
    {
        bool isWeakness = victim.Weaknesses.Contains(attackType);
        bool isBreakingPoint = victim.IsInBreakingPoint;
        if (isWeakness && isBreakingPoint) return 2.0;
        if (isWeakness || isBreakingPoint) return 1.5;
        return 1.0;
    }

    public static (int damage, bool enteredBreakingPoint) Calculate(
        Traveler actor, Beast victim, string attackType, double modifier)
    {
        double baseDamage = GetBaseDamage(actor, victim, attackType, modifier);
        bool enteredBreakingPoint = ShieldManager.TryReduceShield(victim, attackType, baseDamage);
        double multiplier = GetDamageMultiplier(victim, attackType);
        int finalDamage = Convert.ToInt32(Math.Floor(baseDamage * multiplier));
        return (Math.Max(finalDamage, 0), enteredBreakingPoint);
    }
}
