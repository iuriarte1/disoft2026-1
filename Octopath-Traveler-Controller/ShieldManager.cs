using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class ShieldManager
{
    public static bool TryReduceShield(Beast victim, string attackType, double baseDamage)
    {
        if (CannotReduceShield(victim, attackType, baseDamage)) return false;
        victim.Shields--;
        if (victim.Shields == 0)
            victim.EnterBreakingPoint();
        return victim.IsInBreakingPoint;
    }

    private static bool CannotReduceShield(Beast victim, string attackType, double baseDamage)
        => !victim.Weaknesses.Contains(attackType) 
           || victim.IsInBreakingPoint 
           || baseDamage <= 0;
}