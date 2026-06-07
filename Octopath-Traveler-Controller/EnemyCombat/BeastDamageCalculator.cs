using Octopath_Traveler_Model;
using Octopath_Traveler.Combat;

namespace Octopath_Traveler.EnemyCombat;

public class BeastDamageCalculator
{
    private const double DefendDamageMultiplier = 0.5;

    public static int Calculate(Beast actor, Traveler victim, Skill skill, string skillType)
    {
        double rawDamage = CalculateRawDamage(actor, victim, skill, skillType);
        double finalDamage = ApplyDefendReductionInDamage(victim, rawDamage);
        return Convert.ToInt32(Math.Floor(Math.Max(finalDamage, 0)));
    }

    private static double CalculateRawDamage(Beast actor, Traveler victim, Skill skill, string skillType)
    {
        bool isPhysical = skillType == "Phys";
        double attack = isPhysical ? actor.BaseStats.PhysicalAttack : actor.BaseStats.ElementalAttack;
        double defense = isPhysical ? victim.BaseStats.PhysicalDefense : victim.BaseStats.ElementalDefense;

        double rawDamage = attack * skill.Modifier - defense;
        rawDamage *= StatEffectMultipliers.AttackMultiplier(actor, isPhysical);
        rawDamage *= StatEffectMultipliers.DefenseMultiplier(victim, isPhysical);
        return rawDamage;
    }

    private static double ApplyDefendReductionInDamage(Traveler victim, double rawDamage)
    {
        return victim.IsDefendingThisRound
            ? rawDamage * DefendDamageMultiplier
            : rawDamage;
    }
}