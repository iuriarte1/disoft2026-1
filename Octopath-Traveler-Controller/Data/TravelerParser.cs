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
            var parsedData = ExtractTravelerDataOfLine(line);
            Traveler template = _travelersDatabase.FirstOrDefault(t => t.Name == parsedData.Name);
            if (template == null)
            {
                _view.InvalidTeamsFileMessage();
                return null; 
            }
            Traveler newTraveler = new Traveler(template);
            Traveler finalTraveler = AddSkillsToTraveler(newTraveler, parsedData.ActiveSkills, parsedData.SupportSkills);
            team.Add(finalTraveler);
        }
        return team;
    }
    private Traveler AddSkillsToTraveler(Traveler traveler, List<string> activeSkillNames, List<string> supportSkillNames)
    {
        SkillParser skillParser = new SkillParser(new List<string>());
        var active = skillParser.GetActiveSkillsForTraveler(activeSkillNames);
        var passive = skillParser.GetPassiveSkillsForTraveler(supportSkillNames);
        traveler.ActiveSkills = active;
        traveler.PasiveSkills = passive;
        return traveler;
    }
    private (string Name, List<string> ActiveSkills, List<string> SupportSkills) ExtractTravelerDataOfLine(string line)
    {
        List<string> activeSkills = ExtractTravelerActiveSkillsNames(line);
        List<string> supportSkills = ExtractTravelerPassiveSkills(line);
        string name = ExtractTravelerNameOfLine(line);
        return (name, activeSkills, supportSkills);
    }
    private string ExtractTravelerNameOfLine(string line)
    {
        int primerParentesis = line.IndexOf('(');
        int primerCorchete = line.IndexOf('[');
        int nameEndIndex = line.Length;
        if (primerParentesis != -1) nameEndIndex = Math.Min(nameEndIndex, primerParentesis);
        if (primerCorchete != -1) nameEndIndex = Math.Min(nameEndIndex, primerCorchete);
        return line.Substring(0, nameEndIndex).Trim();
    }
    private List<string> ExtractTravelerActiveSkillsNames(string line)
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
    private List<string> ExtractTravelerPassiveSkills(string line)
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
}