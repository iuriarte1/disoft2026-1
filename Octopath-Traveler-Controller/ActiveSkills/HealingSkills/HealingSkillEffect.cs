using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public abstract class HealingSkillEffect : IActiveSkillEffect
{
    protected readonly Skill _skill;
    protected HealingSkillEffect(Skill skill)    
    {
        _skill = skill;
    }

    public void Execute(Traveler atacante, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var travelersToHeal  = SelectTravelersToHeal(playerTeam);
        view.ShowSkillUsed(atacante.Name, _skill.Name);
        ApplyHealingToTravelers(atacante, travelersToHeal, view);
    }
    protected abstract List<Traveler> SelectTravelersToHeal(List<Traveler> travelers);

    private void ApplyHealingToTravelers(Traveler atacante, List<Traveler> travelersToHeal, View view)
    {
        foreach (var traveler in travelersToHeal)
        {
            int healCuantity = CalculateHealing(atacante);
            traveler.RestoreHp(healCuantity);
            view.ShowHealingResult(traveler.Name, healCuantity);
        }
        ShowFinalHpOfTravelers(travelersToHeal, view);
    }
    private int CalculateHealing(Traveler atacante)
    {
        double hpToHeal = atacante.BaseStats.ElementalDefense * _skill.Modifier;
        return Convert.ToInt32(Math.Floor(hpToHeal));
    }
    private void ShowFinalHpOfTravelers(List<Traveler> travelersToHeal, View view)
    {
        foreach (var traveler in travelersToHeal)
        {
            view.ShowFinalHp(traveler.Name, traveler.CurrentHp);
        }
    }
}