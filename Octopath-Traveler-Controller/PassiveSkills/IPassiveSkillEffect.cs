using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public interface IPassiveSkillEffect
{
    void OnBattleStart(Traveler owner);
    void OnEndOfRound(Traveler owner);
    
    // quedo para la E4
    void OnTakeDamage(Traveler owner, ref int damage);
    void OnSpend(Traveler owner, ref int spCost);
    //
    
    void OnHeal(Traveler owner, ref int healing);
    void OnDeath(Traveler owner);
    void OnBasicAttack(Traveler owner, int totalDamage);
}