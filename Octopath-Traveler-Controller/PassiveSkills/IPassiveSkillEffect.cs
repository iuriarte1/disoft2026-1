using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public interface IPassiveSkillEffect
{
    void OnBattleStart(Traveler owner);
}