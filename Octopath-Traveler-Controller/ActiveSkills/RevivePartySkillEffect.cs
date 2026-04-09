using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class RevivePartySkillEffect : ReviveSkillEffect
{
    public RevivePartySkillEffect(Skill skill) : base(skill) { }

    protected override List<Traveler> SelectTravelersToRevive(List<Traveler> playerTeam)
        => playerTeam.Where(t => t.IsDead).ToList();
}