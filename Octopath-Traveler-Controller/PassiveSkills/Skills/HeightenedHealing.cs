using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class HeightenedHealing : PassiveSkillEffect
{
    private const double HealBonus = 0.30;
    public override void OnHeal(Traveler owner, ref int healing)
        => healing = (int)(healing * (1 + HealBonus));
}