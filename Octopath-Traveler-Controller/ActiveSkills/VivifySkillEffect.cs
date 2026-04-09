using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class VivifySkillEffect : ReviveSkillEffect 
{
    private readonly Traveler _travelerToRevive;
    public VivifySkillEffect(Skill skill, Traveler travelerToRevive) : base(skill)
    {
        _travelerToRevive = travelerToRevive;
    }
    protected override List<Traveler> SelectTravelersToRevive(List<Traveler> playerTeam)
        => new List<Traveler> { _travelerToRevive };
    // aca falta arreglar que lo cure tambien 
}