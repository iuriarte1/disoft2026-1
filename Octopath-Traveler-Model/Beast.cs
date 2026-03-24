namespace Octopath_Traveler_Model;

public class Beast : Unit
{
    public List<string> Weaknesses { get; set; }
    public List<Skill> Skills { get; set; }
    // shield
    public int Shields { get; set; }
    public override string GetStatsSummary()
    {
        return $"{Name} - HP:{CurrentHp}/{BaseStats.MaxHp} Shields:{Shields}";
    }
}