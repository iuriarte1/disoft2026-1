using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class AugmentedMagicSkillEffect : IBeastSkillEffect
{
    private readonly Skill _skill;
    private readonly List<Beast> _enemyTeam;
    private const int BuffDuration = 2;

    public AugmentedMagicSkillEffect(Skill skill, List<Beast> enemyTeam)
    {
        _skill = skill;
        _enemyTeam = enemyTeam;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        foreach (var beast in _enemyTeam.Where(b => !b.IsDead))
        {
            beast.ApplyStatEffect(StatModifierType.IncreasedElementalAttack, BuffDuration);
            beast.ApplyStatEffect(StatModifierType.IncreasedElementalDefense, BuffDuration);
            view.ShowStatEffectApplied(beast.Name, StatEffectLabel.For(StatModifierType.IncreasedElementalAttack), BuffDuration);
            view.ShowStatEffectApplied(beast.Name, StatEffectLabel.For(StatModifierType.IncreasedElementalDefense), BuffDuration);
        }
    }
}