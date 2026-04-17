using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class AllyHealingSkillEffect :  HealingSkillEffect
{
    private readonly Traveler _allyToHeal;

    public AllyHealingSkillEffect(Skill skill, Traveler allyToHeal) : base(skill)
    {
        _allyToHeal = allyToHeal;
    }
    protected override List<Traveler> SelectTravelersToHeal(List<Traveler> playerTeam)
    {
        return new List<Traveler>{ _allyToHeal };
    }
}