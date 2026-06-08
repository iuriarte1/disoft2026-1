using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class HangTough : PassiveSkillEffect
{
    private const double HpThreshold = 0.10;
    public override void OnTakeDamage(Traveler owner, ref int damage)
    {
        bool isAboveThreshold = owner.CurrentHp > owner.BaseStats.MaxHp * HpThreshold;
        if (isAboveThreshold && owner.CurrentHp - damage <= 0)
            damage = owner.CurrentHp - 1;
    }
}