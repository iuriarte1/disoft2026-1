using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class UserBuffSkillEffect : StatusSkillEffect
{
    public UserBuffSkillEffect(Skill skill, IReadOnlyList<StatModifierType> effects, int baseRounds, int bpUsed, int roundsPerBp)
        : base(skill, effects, baseRounds, bpUsed, roundsPerBp) { }
    protected override Unit SelectTarget(Traveler actor, List<Traveler> p, List<Beast> e) => actor;
}