using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class SilentSingleTargetSkill : SingleTargetOffensiveSkill
{
    public SilentSingleTargetSkill(Skill skill, Beast victim) : base(skill, victim) { }

    public override void Execute(Traveler attacker, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var victims = SelectVictims(enemyTeam);
        ApplyDamageOnly(attacker, victims, view);
    }

    private void ApplyDamageOnly(Traveler attacker, List<Beast> victims, View view)
    {
        foreach (var victim in victims)
        {
            var (damage, enteredBreakingPoint) = CalculateDamage(attacker, victim);
            victim.TakeDamage(damage);
            ShowDamageMessage(victim, damage, view);
            if (enteredBreakingPoint)
                view.ShowBreakingPoint(victim.Name);
        }
    }

    private void ShowDamageMessage(Beast victim, int damage, View view)
    {
        bool isWeakness = victim.Weaknesses.Contains(_skill.Type);
        if (isWeakness)
            view.ShowSkillDamageResultWithWeakness(victim.Name, _skill.Type, damage);
        else
            view.ShowSkillDamageResult(victim.Name, _skill.Type, damage);
    }
}