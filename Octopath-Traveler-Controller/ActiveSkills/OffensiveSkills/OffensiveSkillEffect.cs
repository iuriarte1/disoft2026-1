using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public abstract class OffensiveSkillEffect : IActiveSkillEffect
{
    protected readonly Skill _skill;

    protected OffensiveSkillEffect(Skill skill)
    {
        _skill = skill;
    }

    public virtual void Execute(Traveler attacker, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var victims = SelectVictims(enemyTeam);
        view.ShowSkillUsed(attacker.Name, _skill.Name);
        ApplyDamageToVictims(attacker, victims, view);
        ShowFinalHpOfVictims(victims, view);
    }

    protected abstract List<Beast> SelectVictims(List<Beast> enemyTeam);

    private void ApplyDamageToVictims(Traveler attacker, List<Beast> victims, View view)
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

    protected virtual (int damage, bool enteredBreakingPoint) CalculateDamage(Traveler attacker, Beast victim)
        => ActiveSkillDamageCalculator.Calculate(attacker, victim, _skill);

    private void ShowFinalHpOfVictims(List<Beast> victims, View view)
    {
        foreach (var victim in victims)
            view.ShowFinalHp(victim.Name, victim.CurrentHp);
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