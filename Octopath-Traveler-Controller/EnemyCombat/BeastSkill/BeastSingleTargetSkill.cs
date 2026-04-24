using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastSingleTargetSkill : IBeastSkillEffect
{
    private static Skill _skill;
    private readonly IVictimSelector _victimSelector;
    private readonly string _skillType;
    
    public BeastSingleTargetSkill(Skill skill,string skilltype,  IVictimSelector victimSelector)
    {
        _skill = skill;
        _victimSelector = victimSelector;
        _skillType = skilltype;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        var victim = _victimSelector.SelectVictim(aliveTeam);

        view.ShowSkillUsed(actor.Name, _skill.Name);

        if (victim.IsDefendingThisRound)
            view.ShowTravelerDefending(victim.Name);

        int damage = BeastDamageCalculator.Calculate(actor, victim, _skill, _skillType);
        victim.TakeDamage(damage);
        view.ShowBeastDamage(victim.Name, damage, GetAttackTypeName());
        view.ShowFinalHp(victim.Name, victim.CurrentHp);
    }

    private string GetAttackTypeName()
    {
        return _skillType == "Phys" ? "físico" : "elemental";
    }
}