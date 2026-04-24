using System.Text.Json.Serialization;

namespace Octopath_Traveler_Model;

public class Traveler : Unit
{
    public List<string> Weapons { get; set; } = new List<string>();
    public List<Skill> ActiveSkills { get; set; } = new List<Skill>();
    public List<Skill> PasiveSkills { get; set; } = new List<Skill>();

    public List<string> ActionOptions => new List<string>
        { "Ataque básico", "Usar habilidad", "Defender", "Huir" };

    [JsonConstructor]
    public Traveler()
    {
        SetInitialBp(1);
    }

    public Traveler(Traveler template)
    {
        Name = template.Name;
        BaseStats = template.BaseStats;
        ActiveSkills = new List<Skill>(template.ActiveSkills);
        PasiveSkills = new List<Skill>(template.PasiveSkills);
        Weapons = new List<string>(template.Weapons);
        InitializeCurrentStats();
        SetInitialBp(1);
    }

    public void PrepareForNewRound()
    {
        HasTurnPriorityThisRound   = HasTurnPriorityFromSkill;
        HasDefendPriorityThisRound = HasDefendPriorityNextRound;
        HasDefendPriorityNextRound = false;
        HasTurnPriorityFromSkill   = false;
    }

    public void EndOfRoundCleanUp()
    {
        IsDefendingThisRound       = false;
        HasTurnPriorityThisRound   = false;
        RevivedThisRound           = false;
        HasDefendPriorityThisRound = false;
    }
}