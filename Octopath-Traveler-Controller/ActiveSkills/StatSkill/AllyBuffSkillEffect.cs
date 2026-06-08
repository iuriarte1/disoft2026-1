using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class AllyBuffSkillEffect : StatusSkillEffect
{
    private readonly Traveler _ally;
    public AllyBuffSkillEffect(Skill skill, Traveler ally, IReadOnlyList<StatModifierType> effects, int baseRounds, int bpUsed, int roundsPerBp)
        : base(skill, effects, baseRounds, bpUsed, roundsPerBp) { _ally = ally; }
    protected override Unit SelectTarget(Traveler actor, List<Traveler> p, List<Beast> e) => _ally;
}