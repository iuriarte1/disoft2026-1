using System.Text.Json.Serialization;
namespace Octopath_Traveler_Model;

public class Unit
{
    public string Name { get; set; }
    [JsonPropertyName("Stats")]
    public Stats BaseStats { get; set; }
    public int CurrentHp { get; set; }
    public int CurrentSp { get; set; }
    public int CurrentBp { get; set; } 
    public bool IsDead => CurrentHp <= 0;
    public void TakeDamage(int damageAmount)
    {
        CurrentHp -= damageAmount;
        if (CurrentHp < 0) CurrentHp = 0;
    }
    public virtual string GetStatsSummary()
    {
        return $"{Name} - HP:{CurrentHp}/{BaseStats.MaxHp} SP:{CurrentSp}/{BaseStats.MaxSp} BP:{CurrentBp}";
    }

    
}