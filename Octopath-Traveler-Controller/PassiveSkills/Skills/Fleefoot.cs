using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class Fleefoot : PassiveSkillEffect
{
    private int boostSpeed = 50;
    public override void OnBattleStart(Traveler owner)
    {
        owner.BaseStats.Speed += boostSpeed;
    }
}