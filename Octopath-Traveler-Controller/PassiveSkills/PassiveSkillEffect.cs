using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class PassiveSkillEffect : IPassiveSkillEffect
{
    public virtual void OnBattleStart(Traveler owner) { }
    public virtual void OnEndOfRound(Traveler owner) { }
    public virtual void OnTakeDamage(Traveler owner, ref int damage) { }
    public virtual void OnSpend(Traveler owner, ref int spCost) { }
    public virtual void OnHeal(Traveler owner, ref int healing) { }
    public virtual void OnDeath(Traveler owner) { }
    public virtual void OnBasicAttack(Traveler owner, int totalDamage) { }
}