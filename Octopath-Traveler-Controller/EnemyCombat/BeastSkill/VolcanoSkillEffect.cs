using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class VolcanoSkillEffect : IBeastSkillEffect
{
    private readonly Skill _skill;
    private const int DebuffDuration = 2;

    public VolcanoSkillEffect(Skill skill)
    {
        _skill = skill;
    }
    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        ApplyEffect(actor, aliveTeam, view);
    }
    private void ApplyDamageToVictim(Beast actor, Traveler victim, View view)
    {
            if (victim.IsDefendingThisRound)
                view.ShowTravelerDefending(victim.Name);
            int damage = BeastDamageCalculator.Calculate(actor, victim, _skill, "Elem");
            victim.TakeDamageFromUnit(damage, actor);
            view.ShowBeastDamage(victim.Name, damage, "elemental");
    }
    private void ShowFinalHpOfTeam(List<Traveler> aliveTeam, View view)
    {
        foreach (var victim in aliveTeam)
            view.ShowFinalHp(victim.Name, victim.CurrentHp);
    }
    private void ApplyDebuffToVictim(Traveler victim, View view)
    {
        victim.ApplyStatEffect(StatModifierType.DecreasedElementalDefense, DebuffDuration);
        view.ShowStatEffectApplied(victim.Name, 
            StatEffectLabel.For(StatModifierType.DecreasedElementalDefense), DebuffDuration);
    }

    private void ApplyEffect(Beast actor,List<Traveler> aliveTeam, View view)
    {
        foreach (var victim in aliveTeam)
        {
            ApplyDamageToVictim(actor, victim, view);
            ApplyDebuffToVictim(victim, view);
        }
        ShowFinalHpOfTeam(aliveTeam, view);
    }
}