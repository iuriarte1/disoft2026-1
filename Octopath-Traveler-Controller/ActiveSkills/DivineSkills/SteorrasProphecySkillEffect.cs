using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class SteorrasProphecySkillEffect : IActiveSkillEffect
{
    private const double BpModifierBonus = 0.20;
    private readonly Skill _skill;
    private readonly List<Traveler> _playerTeam;

    public SteorrasProphecySkillEffect(Skill skill, List<Traveler> playerTeam)
    {
        _skill = skill;
        _playerTeam = playerTeam;
    }

    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        double modifier = CalculateModifier();
        var scaledSkill = new Skill 
        { 
            Name = _skill.Name, Type = _skill.Type, 
            Modifier = modifier, Target = _skill.Target 
        };
        foreach (var enemy in enemyTeam.Where(e => !e.IsDead))
        {
            var (damage, enteredBreakingPoint) = ActiveSkillDamageCalculator.Calculate(actor, enemy, scaledSkill);
            enemy.TakeDamage(damage);
            bool isWeakness = enemy.Weaknesses.Contains(_skill.Type);
            if (isWeakness)
                view.ShowSkillDamageResultWithWeakness(enemy.Name, _skill.Type, damage);
            else
                view.ShowSkillDamageResult(enemy.Name, _skill.Type, damage);
            if (enteredBreakingPoint) view.ShowBreakingPoint(enemy.Name);
        }
        foreach (var enemy in enemyTeam)
            view.ShowFinalHp(enemy.Name, enemy.CurrentHp);
    }

    private double CalculateModifier()
    {
        int totalBp = _playerTeam.Sum(t => t.CurrentBp);
        return _skill.Modifier + BpModifierBonus * totalBp;
    }
}