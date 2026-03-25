namespace Octopath_Traveler_Model;

public class Traveler : Unit
{
    
    
    // armas ??
    public List<string> Weapons { get; set; } = new List<string>();
    public List<Skill> ActiveSkills { get; set; } = new List<Skill>();
    public List<Skill> PasiveSkills { get; set; } = new List<Skill>();
    public Traveler()
    {
        CurrentBp = 1;
    }

    public List<string> Optionsattack = ["Ataque básico", "Usar habilidad", "Defender", "Huir"];

}