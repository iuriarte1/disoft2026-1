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

    public void Execute(Traveler atacante, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var victims = SelectVictims(enemyTeam);
        view.ShowSkillUsed(atacante.Name, _skill.Name);
        ApplyDamageToVictims(atacante, victims, view);
    }

    protected abstract List<Beast> SelectVictims(List<Beast> enemyTeam);
    private void ApplyDamageToVictims(Traveler atacante, List<Beast> victims, View view)
    {
        foreach (var victim in victims)
        {
            var (damage, enteredBreakingPoint) = ActiveSkillDamageCalculator.Calculate(atacante, victim, _skill);
            victim.TakeDamage(damage);
            ShowDamageMessage(victim, damage, view);
            if (enteredBreakingPoint)
            {
                view.ShowBreakingPoint(victim.Name);
            }
        }

        ShowFinalHpOfVictims(victims, view);
    }
    private void ShowFinalHpOfVictims(List<Beast> victims, View view)
    {
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