using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class ConsumeArmourSkillEffect : IBeastSkillEffect
{
    private readonly Skill _skill;
    private const int DebuffDuration = 2;

    public ConsumeArmourSkillEffect(Skill skill)
    {
        _skill = skill;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        var victim = new TravelerWithHighestPhysDef().SelectVictim(aliveTeam);
        view.ShowSkillUsed(actor.Name, _skill.Name);
        victim.ApplyStatEffect(StatModifierType.DecreasedPhysicalDefense, DebuffDuration);
        view.ShowStatEffectApplied(victim.Name, StatEffectLabel.For(StatModifierType.DecreasedPhysicalDefense), DebuffDuration);
    }
}