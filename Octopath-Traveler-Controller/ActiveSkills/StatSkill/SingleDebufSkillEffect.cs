using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class SingleDebuffSkillEffect : StatusSkillEffect
{
    private readonly Beast _victim;
    public SingleDebuffSkillEffect(Skill skill, Beast victim, IReadOnlyList<StatModifierType> effects, int rounds)
        : base(skill, effects, rounds) { _victim = victim; }
    protected override Unit SelectTarget(Traveler actor, List<Traveler> p, List<Beast> e) => _victim;
}