using Octopath_Traveler_Model;

namespace Octopath_Traveler.Combat;

public class StatEffectMultipliers
{
    private const double IncreasedAttackFactor = 1.5;
    private const double DecreasedAttackFactor = 2.0 / 3.0;
    private const double IncreasedDefenseFactor = 2.0 / 3.0;
    private const double DecreasedDefenseFactor = 1.5;

    public static double AttackMultiplier(Unit attacker, bool isPhysical)
    {
        var increased = isPhysical
            ? StatModifierType.IncreasedPhysicalAttack
            : StatModifierType.IncreasedElementalAttack;
        var decreased = isPhysical
            ? StatModifierType.DecreasedPhysicalAttack
            : StatModifierType.DecreasedElementalAttack;
        return FactorFor(attacker, increased, IncreasedAttackFactor)
               * FactorFor(attacker, decreased, DecreasedAttackFactor);
    }

    public static double DefenseMultiplier(Unit defender, bool isPhysical)
    {
        var increased = isPhysical
            ? StatModifierType.IncreasedPhysicalDefense
            : StatModifierType.IncreasedElementalDefense;
        var decreased = isPhysical
            ? StatModifierType.DecreasedPhysicalDefense
            : StatModifierType.DecreasedElementalDefense;
        return FactorFor(defender, increased, IncreasedDefenseFactor)
               * FactorFor(defender, decreased, DecreasedDefenseFactor);
    }

    private static double FactorFor(Unit unit, StatModifierType type, double factor)
        => unit.HasStatEffect(type) ? factor : 1.0;
}