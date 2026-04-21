using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class InnerStrength : PassiveSkillEffect
{
    private int boostSpMax = 50;
    public override void OnBattleStart(Traveler owner)
    {
        owner.BaseStats.MaxSp += boostSpMax;
        owner.CurrentSp += boostSpMax;
    }
}