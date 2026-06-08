using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.VictimSelection;
using Octopath_Traveler.PassiveSkills;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastSingleTargetSkill : IBeastSkillEffect
{
    private readonly Skill _skill;
    private readonly IVictimSelector _victimSelector;
    private readonly string _skillType;
    private readonly PassiveSkillManager _passiveManager;

    public BeastSingleTargetSkill(Skill skill, string skillType, IVictimSelector victimSelector, PassiveSkillManager passiveManager)
    {
        _skill = skill;
        _skillType = skillType;
        _victimSelector = victimSelector;
        _passiveManager = passiveManager;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        var victim = _victimSelector.SelectVictim(aliveTeam);
        view.ShowSkillUsed(actor.Name, _skill.Name);
        if (victim.IsDefendingThisRound)
            view.ShowTravelerDefending(victim.Name);
        for (int i = 0; i < _skill.Hits; i++)
        {
            int damage = BeastDamageCalculator.Calculate(actor, victim, _skill, _skillType);
            victim.TakeDamageFromUnit(damage, actor);
            view.ShowBeastDamage(victim.Name, damage, GetAttackTypeName());
        }
        if (victim.IsDead)
            _passiveManager.ApplyOnDeathEffects(victim);
        view.ShowFinalHp(victim.Name, victim.CurrentHp);
    }

    private string GetAttackTypeName()
        => _skillType == "Phys" ? "físico" : "elemental";
}