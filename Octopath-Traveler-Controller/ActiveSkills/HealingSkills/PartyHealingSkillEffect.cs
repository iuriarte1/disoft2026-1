using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class PartyHealingSkillEffect : HealingSkillEffect
{
    private readonly Traveler _actor;

    public PartyHealingSkillEffect(Skill skill, Traveler actor) : base(skill)
    {
        _actor = actor;
    }

    protected override List<Traveler> SelectTravelersToHeal(List<Traveler> playerTeam)
    {
        return playerTeam
            .Where(t => !t.IsDead && t != _actor)
            .Concat(new[] { _actor })
            .ToList();
    }
        
}