using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class StatSwap : PassiveSkillEffect
{
    public override void OnBattleStart(Traveler owner)
        => owner.BaseStats.SwapPhysAndElemAtk();
}