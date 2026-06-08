using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class Inspiration : PassiveSkillEffect
{
    private const double SpFactor = 0.01;
    public override void OnBasicAttack(Traveler owner, int totalDamage)
        => owner.RestoreSp((int)(totalDamage * SpFactor));
}