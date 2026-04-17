using Octopath_Traveler_Model;
using Octopath_Traveler_View;
namespace Octopath_Traveler.ActiveSkills;

public abstract class OffensiveSkillEffect : IActiveSkillEffect
{
    protected readonly Skill _skill;
    protected readonly bool _showSkillUsed;
    protected readonly bool _showFinalHp;
    protected OffensiveSkillEffect(Skill skill, bool showSkillUsed = true, bool showFinalHp = true)
    {
        _skill = skill;
        _showSkillUsed = showSkillUsed;
        _showFinalHp = showFinalHp;
    }

    public virtual void Execute(Traveler atacante, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var victims = SelectVictims(enemyTeam);
        if (_showSkillUsed)
            view.ShowSkillUsed(atacante.Name, _skill.Name);
        ApplyDamageToVictims(atacante, victims, view);
    }

    protected abstract List<Beast> SelectVictims(List<Beast> enemyTeam);
    private void ApplyDamageToVictims(Traveler atacante, List<Beast> victims, View view)
    {
        foreach (var victim in victims)
        {
            var (damage, enteredBreakingPoint) = CalculateDamage(atacante, victim);
            victim.TakeDamage(damage);
            ShowDamageMessage(victim, damage, view);
            if (enteredBreakingPoint)
            {
                view.ShowBreakingPoint(victim.Name);
            }
        }

        ShowFinalHpOfVictims(victims, view);
    }

    protected virtual (int damage, bool enteredBreakingPoint) CalculateDamage(Traveler atacante, Beast victim)
    {
        return ActiveSkillDamageCalculator.Calculate(atacante, victim, _skill);
    }
    private void ShowFinalHpOfVictims(List<Beast> victims, View view)
    {
        if (!_showFinalHp) return;
        foreach (var victim in victims)
        {
            view.ShowFinalHp(victim.Name, victim.CurrentHp);
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