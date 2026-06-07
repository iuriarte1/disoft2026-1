using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class AcidSpraySkillEffect : IBeastSkillEffect
{
    private readonly Skill _skill;
    private const int DebuffDuration = 2;

    public AcidSpraySkillEffect(Skill skill)
    {
        _skill = skill;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        var victim = new TravelerWithHighestHp().SelectVictim(aliveTeam);
        view.ShowSkillUsed(actor.Name, _skill.Name);
        ApplyDebuff(victim, StatModifierType.DecreasedPhysicalDefense, view);
        ApplyDebuff(victim, StatModifierType.DecreasedElementalDefense, view);
    }

    private void ApplyDebuff(Traveler victim, StatModifierType debuff, View view)
    {
        victim.ApplyStatEffect(debuff, DebuffDuration);
        view.ShowStatEffectApplied(victim.Name, StatEffectLabel.For(debuff), DebuffDuration);
    }
}