using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class VimAndVigor : PassiveSkillEffect
{
    private const double HealFactor = 0.10;
    public override void OnEndOfRound(Traveler owner)
        => owner.RestoreHp((int)(owner.BaseStats.MaxHp * HealFactor));
}