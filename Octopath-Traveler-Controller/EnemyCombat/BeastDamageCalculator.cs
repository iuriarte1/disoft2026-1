using Octopath_Traveler_Model;

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
        return skillType == "Phys"
            ? actor.BaseStats.PhysicalAttack * skill.Modifier - victim.BaseStats.PhysicalDefense
            : actor.BaseStats.ElementalAttack * skill.Modifier - victim.BaseStats.ElementalDefense;
    }

    private static double ApplyDefendReductionInDamage(Traveler victim, double rawDamage)
    {
        return victim.IsDefendingThisRound
            ? rawDamage * DefendDamageMultiplier
            : rawDamage;
    }
}