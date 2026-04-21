using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public abstract class PassiveSkillEffect : IPassiveSkillEffect
{
    public virtual void OnBattleStart(Traveler owner) { }
}