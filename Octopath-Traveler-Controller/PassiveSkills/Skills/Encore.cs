using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class Encore : PassiveSkillEffect
{
    private bool _used = false;
    private const double ReviveFactor = 0.25;
    public override void OnDeath(Traveler owner)
    {
        if (_used) return;
        _used = true;
        owner.ReviveWithHp((int)(owner.BaseStats.MaxHp * ReviveFactor));
    }
}