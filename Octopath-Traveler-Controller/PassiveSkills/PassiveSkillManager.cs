using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class PassiveSkillManager
{
    private readonly List<Traveler> _playerTeam;

    public PassiveSkillManager(List<Traveler> playerTeam)
    {
        _playerTeam = playerTeam;
    }

    public void ApplyBattleStartEffects()
    {
        foreach (var traveler in _playerTeam)
        foreach (var skill in traveler.PasiveSkills)
            PassiveSkillFactory.Create(skill).OnBattleStart(traveler);
    }
}