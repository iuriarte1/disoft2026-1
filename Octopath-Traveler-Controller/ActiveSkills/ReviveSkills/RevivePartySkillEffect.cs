using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class RevivePartySkillEffect : ReviveSkillEffect
{
    private readonly int _bpUsed;
    private const double ReviveHealModifier = 1.4;

    public RevivePartySkillEffect(Skill skill, int bpUsed) : base(skill)
    {
        _bpUsed = bpUsed;
    }

    protected override List<Traveler> SelectTravelersToRevive(List<Traveler> playerTeam)
        => playerTeam.Where(t => t.IsDead).ToList();

    public override void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var travelersToRevive = SelectTravelersToRevive(playerTeam);
        view.ShowSkillUsed(actor.Name, _skill.Name);
        foreach (var traveler in travelersToRevive)
        {
            traveler.Revive();
            traveler.MarkAsRevived();
            view.ShowReviveResult(traveler.Name);
            if (_bpUsed > 0)
            {
                int healing = CalculateHealing(actor);
                traveler.RestoreHp(healing);
                view.ShowHealingResult(traveler.Name, healing);
            }
        }
        ShowFinalHpOfTeam(travelersToRevive, view);
    }

    private void ShowFinalHpOfTeam(List<Traveler> travelersToRevive, View view)
    {
        foreach (var traveler in travelersToRevive)
            view.ShowFinalHp(traveler.Name, traveler.CurrentHp);
    }

    private int CalculateHealing(Traveler actor)
        => Convert.ToInt32(Math.Floor(actor.BaseStats.ElementalDefense * ReviveHealModifier * _bpUsed));
}