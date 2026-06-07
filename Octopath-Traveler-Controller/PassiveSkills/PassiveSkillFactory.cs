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
        "Summon Strength" => new SummonStrength(),
        "Vim and Vigor" => new VimAndVigor(),
        "Second Wind" => new SecondWind(),
        "Boost Start" => new BoostStart(),
        "Stat Swap" => new StatSwap(),
        "Hang Tough" => new HangTough(),
        "SP Saver" => new SpSaver(),
        "Heightened Healing" => new HeightenedHealing(),
        "Encore" => new Encore(),
        "Inspiration" => new Inspiration(),
        _                        => new PassiveSkillEffect()
    };
}