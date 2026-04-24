using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class HaleAndHearty : PassiveSkillEffect
{
    private const int HpBonus = 500;
    public override void OnBattleStart(Traveler owner)
    {
        owner.IncreaseMaxHp(HpBonus);
    }
}