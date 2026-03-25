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
    public override string GetStatsSummary()
    {
        return $"{Name} - HP:{CurrentHp}/{BaseStats.MaxHp} Shields:{Shields}";
    }
}