using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills.DivineSkills;

public class BalogarsBladSkillEffect : IActiveSkillEffect
{
    private static readonly string[] ElementTypes = 
        { "Fire", "Ice", "Lightning", "Wind", "Light", "Dark" };
    private readonly Skill _skill;
    private readonly Beast _victim;

    public BalogarsBladSkillEffect(Skill skill, Beast victim)
    {
        _skill = skill;
        _victim = victim;
    }

    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        foreach (var elementType in ElementTypes)
            ApplyHit(actor, elementType, view);
        view.ShowFinalHp(_victim.Name, _victim.CurrentHp);
    }

    private void ApplyHit(Traveler actor, string elementType, View view)
    {
        var fakeSkill = new Skill 
        { 
            Name = _skill.Name, Type = elementType, 
            Modifier = _skill.Modifier, Target = "Single" 
        };
        var (damage, enteredBreakingPoint) = ActiveSkillDamageCalculator.Calculate(actor, _victim, fakeSkill);
        _victim.TakeDamage(damage);
        bool isWeakness = _victim.Weaknesses.Contains(elementType);
        if (isWeakness)
            view.ShowSkillDamageResultWithWeakness(_victim.Name, elementType, damage);
        else
            view.ShowSkillDamageResult(_victim.Name, elementType, damage);
        if (enteredBreakingPoint) view.ShowBreakingPoint(_victim.Name);
    }
}