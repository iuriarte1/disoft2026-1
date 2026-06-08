using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class SpSaver : PassiveSkillEffect
{
    public override void OnSpend(Traveler owner, ref int spCost)
        => spCost = (int)Math.Ceiling(spCost / 2.0);
}