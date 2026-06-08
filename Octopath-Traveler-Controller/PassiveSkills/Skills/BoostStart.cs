using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class BoostStart : PassiveSkillEffect
{
    public override void OnBattleStart(Traveler owner)
        => owner.GainBp(1);
}