using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class SecondWind : PassiveSkillEffect
{
    private const double RestoreFactor = 0.05;
    public override void OnEndOfRound(Traveler owner)
        => owner.RestoreSp((int)(owner.BaseStats.MaxSp * RestoreFactor));
}