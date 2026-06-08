using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public abstract class StatusSkillEffect : IActiveSkillEffect
{
    protected readonly Skill _skill;
    private readonly IReadOnlyList<StatModifierType> _effects;
    private readonly int _totalRounds;


    protected StatusSkillEffect(Skill skill, IReadOnlyList<StatModifierType> effects, int baseRounds, int bpUsed, int roundsPerBp)
    {
        _skill = skill;
        _effects = effects;
        _totalRounds = baseRounds + roundsPerBp * bpUsed;
    }

    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        Unit target = SelectTarget(actor, playerTeam, enemyTeam);
        ApplyEffectsTo(target, view);
    }

    protected abstract Unit SelectTarget(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam);

    private void ApplyEffectsTo(Unit target, View view)
    {
        foreach (var effect in _effects)
        {
            target.ApplyStatEffect(effect, _totalRounds);
            view.ShowStatEffectApplied(target.Name, StatEffectLabel.For(effect), _totalRounds);
        }
    }
}