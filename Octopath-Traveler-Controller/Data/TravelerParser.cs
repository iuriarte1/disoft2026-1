using Octopath_Traveler_Model;
using Octopath_Traveler_View;
namespace Octopath_Traveler.Data;

public class TravelerParser
{
    private readonly View _view;
    private readonly List<Traveler> _travelersDatabase;

    public TravelerParser(View view, List<Traveler> travelersDatabase)
    {
        _view = view;
        _travelersDatabase = travelersDatabase;
    }
    public List<Traveler> ParseTeam(List<string> lines)
    {
        List<Traveler> team = new();
        foreach (string line in lines)
        {
            var parsedData = ExtractData(line);
            Traveler template = _travelersDatabase.FirstOrDefault(t => t.Name == parsedData.Name);
            
            if (template == null)
            {
                _view.InvalidTeamsFileMessage();
                return null; 
            }
            Traveler newTraveler = CloneTraveler(template);
            Traveler finalTraveler = AddSkillsToTraveler(newTraveler, parsedData.ActiveSkills, parsedData.SupportSkills);
            if (finalTraveler == null)
            {
                // Si la asignación de habilidades falló, propagamos el fallo al llamador
                return null;
            }
            team.Add(finalTraveler);
        }
        return team;
    }
    private Traveler AddSkillsToTraveler(Traveler traveler, List<string> activeSkillNames, List<string> supportSkillNames)
    {
        SkillParser skillParser = new SkillParser(new List<string>());
        var active = skillParser.GetActiveSkillsForTraveler(activeSkillNames);
        var passive = skillParser.GetPassiveSkillsForTraveler(supportSkillNames);

        if (active == null || passive == null)
        {
            // Indica al llamador que el archivo de equipos es inválido
            _view.InvalidTeamsFileMessage();
            return null;
        }

        traveler.ActiveSkills = active;
        traveler.PasiveSkills = passive;
        return traveler;
    }
    private (string Name, List<string> ActiveSkills, List<string> SupportSkills) ExtractData(string line)
    {
        List<string> activeSkills = ExtractActiveSkills(line);
        List<string> supportSkills = ExtractSupportSkills(line);
        string name = ExtractName(line);
        return (name, activeSkills, supportSkills);
    }
    private string ExtractName(string line)
    {
        int primerParentesis = line.IndexOf('(');
        int primerCorchete = line.IndexOf('[');
        int nameEndIndex = line.Length;
        if (primerParentesis != -1) nameEndIndex = Math.Min(nameEndIndex, primerParentesis);
        if (primerCorchete != -1) nameEndIndex = Math.Min(nameEndIndex, primerCorchete);
        return line.Substring(0, nameEndIndex).Trim();
    }
    private List<string> ExtractActiveSkills(string line)
    {
        List<string> activeSkills = new();
        int firstParen = line.IndexOf('(');
        if (firstParen != -1)
        {
            int lastParen = line.IndexOf(')');
            if (lastParen > firstParen)
            {
                string skillsRaw = line.Substring(firstParen + 1, lastParen - firstParen - 1);
                activeSkills = skillsRaw.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList();
            }
        }
        return activeSkills;
    }
    private List<string> ExtractSupportSkills(string line)
    {
        List<string> supportSkills = new();
        int firstBracket = line.IndexOf('[');
        if (firstBracket != -1)        {
            int lastBracket = line.IndexOf(']');
            if (lastBracket > firstBracket)            {
                string skillsRaw = line.Substring(firstBracket + 1, lastBracket - firstBracket - 1);
                supportSkills = skillsRaw.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList();
            }
        }
        return supportSkills;
    }
    private Traveler CloneTraveler(Traveler template)
    {
        // Clonación defensiva: copiamos los objetos y manejamos posibles nulls
        Stats baseStatsCopy = template.BaseStats != null
            ? new Stats
            {
                MaxHp = template.BaseStats.MaxHp,
                MaxSp = template.BaseStats.MaxSp,
                PhysicalAttack = template.BaseStats.PhysicalAttack,
                PhysicalDefense = template.BaseStats.PhysicalDefense,
                ElementalAttack = template.BaseStats.ElementalAttack,
                ElementalDefense = template.BaseStats.ElementalDefense,
                Speed = template.BaseStats.Speed,
                Evasion = template.BaseStats.Evasion
            }
            : new Stats { MaxHp = 0, MaxSp = 0 };

        return new Traveler
        {
            Name = template.Name,
            BaseStats = baseStatsCopy,
            ActiveSkills = template.ActiveSkills != null ? new List<Skill>(template.ActiveSkills) : new List<Skill>(),
            PasiveSkills = template.PasiveSkills != null ? new List<Skill>(template.PasiveSkills) : new List<Skill>(),
            CurrentHp = baseStatsCopy.MaxHp,
            CurrentSp = baseStatsCopy.MaxSp,
            Weapons = template.Weapons != null ? new List<string>(template.Weapons) : new List<string>()
        };
    }
}