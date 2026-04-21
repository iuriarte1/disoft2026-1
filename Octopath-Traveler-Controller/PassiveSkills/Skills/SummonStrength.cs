using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class SummonStrength : PassiveSkillEffect
{
    private int boostStatPhysAtk = 50;
     public override void OnBattleStart(Traveler owner)
    {
        owner.BaseStats.PhysicalAttack += boostStatPhysAtk;
    }
}