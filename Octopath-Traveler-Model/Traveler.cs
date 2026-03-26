using System.Text.Json.Serialization;

namespace Octopath_Traveler_Model;

public class Traveler : Unit
{
    public List<string> Weapons { get; set; } = new List<string>();
    public List<Skill> ActiveSkills { get; set; } = new List<Skill>();
    public List<Skill> PasiveSkills { get; set; } = new List<Skill>();
    [JsonConstructor]
    public Traveler()
    {
        CurrentBp = 1;
    }
    public List<string> Optionsattack = ["Ataque básico", "Usar habilidad", "Defender", "Huir"];
    public Traveler(Traveler template)
    {
        Name = template.Name;
        BaseStats = template.BaseStats;
        ActiveSkills = new List<Skill>(template.ActiveSkills);
        PasiveSkills = new List<Skill>(template.PasiveSkills);
        Weapons = new List<string>(template.Weapons);
        CurrentHp = BaseStats.MaxHp;
        CurrentSp = BaseStats.MaxSp;
        CurrentBp = 1;
    }
    

}