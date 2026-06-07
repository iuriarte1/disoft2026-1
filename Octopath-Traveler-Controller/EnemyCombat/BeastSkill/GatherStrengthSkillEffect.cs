using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class GatherStrengthSkillEffect : IBeastSkillEffect
{
    private readonly Skill _skill;
    private const int BuffDuration = 2;

    public GatherStrengthSkillEffect(Skill skill)
    {
        _skill = skill;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        var victim = new TravelerWithLowestPhysDef().SelectVictim(aliveTeam);
        view.ShowSkillUsed(actor.Name, _skill.Name);
        if (victim.IsDefendingThisRound)
            view.ShowTravelerDefending(victim.Name);
        int damage = BeastDamageCalculator.Calculate(actor, victim, _skill, "Phys");
        victim.TakeDamageFromUnit(damage, actor);
        view.ShowBeastDamage(victim.Name, damage, "físico");
        actor.ApplyStatEffect(StatModifierType.IncreasedPhysicalAttack, BuffDuration);
        view.ShowStatEffectApplied(actor.Name, StatEffectLabel.For(StatModifierType.IncreasedPhysicalAttack), BuffDuration);
        view.ShowFinalHp(victim.Name, victim.CurrentHp);
    }
}