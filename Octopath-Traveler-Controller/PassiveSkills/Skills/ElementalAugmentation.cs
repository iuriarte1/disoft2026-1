using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class ElementalAugmentation : PassiveSkillEffect
{
    private int boostStatElemAtk = 50;
     public override void OnBattleStart(Traveler owner)
    {
        owner.BaseStats.ElementalAttack += boostStatElemAtk;
    }
}