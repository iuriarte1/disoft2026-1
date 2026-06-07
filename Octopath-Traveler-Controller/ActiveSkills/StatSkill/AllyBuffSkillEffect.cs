using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class AllyBuffSkillEffect : StatusSkillEffect
{
    private readonly Traveler _ally;
    public AllyBuffSkillEffect(Skill skill, Traveler ally, IReadOnlyList<StatModifierType> effects, int rounds)
        : base(skill, effects, rounds) { _ally = ally; }
    protected override Unit SelectTarget(Traveler actor, List<Traveler> p, List<Beast> e) => _ally;
}