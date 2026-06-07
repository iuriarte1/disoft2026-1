using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class ElementalBreakSkillEffect: IActiveSkillEffect
{
    private readonly Skill _skill;
    private readonly Beast _victim;
    private readonly StatModifierType _debuffType;
    private readonly int _debuffRounds;

    public ElementalBreakSkillEffect(
        Skill skill, Beast victim, StatModifierType debuffType, int debuffRounds)
    {
        _skill = skill;
        _victim = victim;
        _debuffType = debuffType;
        _debuffRounds = debuffRounds;
    }
    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        ApplyDamage(actor, view);
        ApplyDebuff(view);
        view.ShowFinalHp(_victim.Name, _victim.CurrentHp);
    }
    private void ApplyDamage(Traveler actor, View view)
    {
        var (damage, enteredBreakingPoint) = ActiveSkillDamageCalculator.Calculate(actor, _victim, _skill);
        _victim.TakeDamage(damage);
        bool isWeakness = _victim.Weaknesses.Contains(_skill.Type);
        if (isWeakness)
            view.ShowSkillDamageResultWithWeakness(_victim.Name, _skill.Type, damage);
        else
            view.ShowSkillDamageResult(_victim.Name, _skill.Type, damage);
        if (enteredBreakingPoint)
            view.ShowBreakingPoint(_victim.Name);
    }
    private void ApplyDebuff(View view)
    {
        _victim.ApplyStatEffect(_debuffType, _debuffRounds);
        view.ShowStatEffectApplied(_victim.Name, StatEffectLabel.For(_debuffType), _debuffRounds);
    }
}