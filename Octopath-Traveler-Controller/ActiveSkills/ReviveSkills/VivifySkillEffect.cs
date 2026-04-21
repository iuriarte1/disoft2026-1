using Octopath_Traveler_Model;
using Octopath_Traveler_View;

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

    public override void Execute(Traveler user, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(user.Name, _skill.Name);
        _travelerToRevive.Revive();
        view.ShowReviveResult(_travelerToRevive.Name);
        int healing = CalculateHpToRestore(user);
        _travelerToRevive.RestoreHp(healing);
        view.ShowHealingResult(_travelerToRevive.Name, healing);
        view.ShowFinalHp(_travelerToRevive.Name, _travelerToRevive.CurrentHp);
        _travelerToRevive.RevivedThisRound = true;
    }
    private int CalculateHpToRestore(Traveler user)
    {
        var hp = Math.Floor(user.BaseStats.ElementalDefense * _skill.Modifier);
        return Convert.ToInt32(hp);
    }
}