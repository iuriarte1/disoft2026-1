using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class ElementalAugmentation : PassiveSkillEffect
{
    private const int BoostStatElemAtk = 50;
    public override void OnBattleStart(Traveler owner)
        => owner.BaseStats.ElementalAttack += BoostStatElemAtk;
}