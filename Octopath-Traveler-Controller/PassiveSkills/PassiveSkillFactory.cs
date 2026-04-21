using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public static class PassiveSkillFactory
{
    public static IPassiveSkillEffect Create(Skill skill) => skill.Name switch
    {
        "Hale and Hearty" => new HaleAndHearty(),
        "Fleefoot" =>  new Fleefoot(),
        "Elemental Augmentation" => new ElementalAugmentation(),
        "Inner Strength" => new InnerStrength(),
        "Summon Strength" => new SummonStrength()
    };
}