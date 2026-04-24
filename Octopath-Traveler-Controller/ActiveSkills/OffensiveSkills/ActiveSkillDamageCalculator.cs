using Octopath_Traveler_Model;
using Octopath_Traveler;

public static class ActiveSkillDamageCalculator
{
    public static (int damage, bool enteredBreakingPoint) Calculate(Traveler actor, Beast victim, Skill skill)
        => DamageCalculator.Calculate(actor, victim, skill.Type, skill.Modifier);
}
