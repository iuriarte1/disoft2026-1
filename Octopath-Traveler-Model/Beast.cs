using System.Text.Json.Serialization;

namespace Octopath_Traveler_Model;

public class Beast : Unit
{
    public List<string> Weaknesses { get; set; }
    [JsonPropertyName("Skill")]
    public string SkillName { get; set; }
    [JsonIgnore]
    public Skill Skill { get; set; }
    // shield
    [JsonPropertyName("Shields")]
    public int Shields { get; set; }
    [JsonIgnore]
    public bool IsInBreakingPoint { get; private set; } = false;

    [JsonIgnore]
    public int MaxShields { get; private set; }
    public override string GetStatsSummary()
    {
        return $"{Name} - HP:{CurrentHp}/{BaseStats.MaxHp} Shields:{Shields}";
    }
    [JsonConstructor]
    public Beast() {}
    public Beast(Beast template)
    {
        Name = template.Name;
        BaseStats = template.BaseStats;
        Weaknesses = template.Weaknesses;
        Skill = template.Skill;
        SkillName = template.SkillName;
        CurrentHp = template.BaseStats.MaxHp;
        CurrentSp = template.BaseStats.MaxSp;
        Shields = template.Shields;
        MaxShields = template.MaxShields;
    }

    public void EnterBreakingPoint()
    {
        IsInBreakingPoint = true;
        Shields = 0;
    }
    public void ExitBreakingPoint()
    {
        IsInBreakingPoint = false;
        Shields = MaxShields;
    }
}