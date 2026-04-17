using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public abstract class ReviveSkillEffect : IActiveSkillEffect
{
    protected readonly Skill _skill;

    protected ReviveSkillEffect(Skill skill)
    {
        _skill = skill;
    }

    public virtual void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var travelersToRevive = SelectTravelersToRevive(playerTeam);
        view.ShowSkillUsed(actor.Name, _skill.Name);
        ReviveTravelers(travelersToRevive, view);
    }
    protected abstract List<Traveler> SelectTravelersToRevive(List<Traveler> playerTeam);
    private void ReviveTravelers(List<Traveler> travelersToRevive, View view)
    {
        foreach (var traveler in travelersToRevive)
        {
            
            traveler.Revive();
            view.ShowReviveResult(traveler.Name);
        }
        ShowFinalHpOfTravelers(travelersToRevive, view);
    }
    private void ShowFinalHpOfTravelers(List<Traveler> travelersToHeal, View view)
    {
        foreach (var traveler in travelersToHeal)
        {
            view.ShowFinalHp(traveler.Name, traveler.CurrentHp);
        }
    }

}