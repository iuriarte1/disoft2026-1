using Octopath_Traveler_Model;

namespace Octopath_Traveler_View;

public class StatEffectLabel
{
    public static string For(StatModifierType type) => type switch
    {
        StatModifierType.IncreasedPhysicalAttack => "Increased Physical Attack",
        StatModifierType.IncreasedPhysicalDefense => "Increased Physical Defense",
        StatModifierType.IncreasedElementalAttack => "Increased Elemental Attack",
        StatModifierType.IncreasedElementalDefense => "Increased Elemental Defense",
        StatModifierType.IncreasedSpeed => "Increased Speed",
        StatModifierType.DecreasedPhysicalAttack => "Decreased Physical Attack",
        StatModifierType.DecreasedPhysicalDefense => "Decreased Physical Defense",
        StatModifierType.DecreasedElementalAttack => "Decreased Elemental Attack",
        StatModifierType.DecreasedElementalDefense => "Decreased Elemental Defense",
        StatModifierType.DecreasedSpeed => "Decreased Speed",
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}