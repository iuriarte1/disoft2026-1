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
    public int RoundsInBreakingPoint { get; private set; } = 0;

    public void EnterBreakingPoint()
    {
        IsInBreakingPoint = true;
        RoundsInBreakingPoint = 2;  // dura la ronda actual + la siguiente
        Shields = 0;
    }
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
    public void InitializeMaxShields()
    {
        MaxShields = Shields;
    }
    public bool JustRecoveredFromBreakingPoint { get; set; } = false;

    public void ExitBreakingPoint()
    {
        IsInBreakingPoint = false;
        Shields = MaxShields;
        JustRecoveredFromBreakingPoint = true; // ← marca la prioridad
    }
    public void TickBreakingPoint()
    {
        RoundsInBreakingPoint--;
        if (RoundsInBreakingPoint == 0)
            ExitBreakingPoint();
    }
}