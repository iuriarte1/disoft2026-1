using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class PartyHealingSkillEffect : HealingSkillEffect
{
    public PartyHealingSkillEffect(Skill skill) : base(skill) { }

    protected override List<Traveler> SelectTravelersToHeal(List<Traveler> playerTeam)
        => playerTeam.Where(t => !t.IsDead).ToList();
}