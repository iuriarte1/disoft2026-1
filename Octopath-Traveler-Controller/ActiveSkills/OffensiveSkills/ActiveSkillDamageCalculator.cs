using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class ActiveSkillDamageCalculator
{
    private static readonly string[] PhysicalTypes = 
        { "Sword", "Spear", "Axe", "Dagger", "Bow", "Stave" };

    public static (int damage, bool enteredBreakingPoint) Calculate(Traveler actor, Beast victim, Skill skill)
    {
        (double rawDamage,bool enteredBreakingPoint )= CalculateRawDamageAndShieldReduction(actor, victim, skill);
        int finalDamage = Convert.ToInt32(Math.Floor(rawDamage));
        return (Math.Max(finalDamage, 0), enteredBreakingPoint);
    }



    public static (double rawDamage, bool enteredBreakingPoint) CalculateRawDamageAndShieldReduction(Traveler actor, Beast victim,
        Skill skill)
    {
        double baseDamage = GetBaseDamage(actor, victim, skill);
        bool enteredBreakingPoint = ShieldManager.TryReduceShield(victim, skill.Type, baseDamage);
        double multiplier = GetDamageMultiplier(victim, skill);
        return (baseDamage * multiplier, enteredBreakingPoint);
    }
    private static double GetBaseDamage(Traveler actor, Beast victim, Skill skill)
    {
        return IsPhysicalSkill(skill) ? CalculatePhysicalDamage(actor, victim, skill.Modifier) : CalculateElementalDamage(actor, victim, skill.Modifier);
    }
    private static bool IsPhysicalSkill(Skill skill)
        => PhysicalTypes.Contains(skill.Type);

    private static double CalculatePhysicalDamage(Traveler actor, Beast victim, double modifier)
        => actor.BaseStats.PhysicalAttack * modifier - victim.BaseStats.PhysicalDefense;

    private static double CalculateElementalDamage(Traveler actor, Beast victim, double modifier)
        => actor.BaseStats.ElementalAttack * modifier - victim.BaseStats.ElementalDefense;

    private static double GetDamageMultiplier(Beast victim, Skill skill)
    {
        bool isWeakness = victim.Weaknesses.Contains(skill.Type);
        bool isBreakingPoint = victim.IsInBreakingPoint;
        if (isWeakness && isBreakingPoint) return 2.0;
        if (isWeakness || isBreakingPoint) return 1.5;
        return 1.0;
    }
    
}

