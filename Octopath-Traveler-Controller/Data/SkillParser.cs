using Octopath_Traveler_Model;
namespace Octopath_Traveler.Data;

public class SkillParser
{
    private List<string> _skills;
    private readonly string _pathActiveSkills = "data/skills.json";
    private readonly string _pathBeastSkills = "data/beast_skills.json";
    private readonly string _pathPassiveSkills = "data/passive_skills.json";
    public SkillParser(List<string> skills)
     {
         _skills = skills;
     }

    public List<Skill> GetActiveSkillsForTraveler(List<string> skillNames)
    {
        return GetSkillsFromFile(skillNames, _pathActiveSkills);
    }
    public List<Skill> GetPassiveSkillsForTraveler(List<string> skillNames)
    {
        return GetSkillsFromFile(skillNames, _pathPassiveSkills);
    }
    public Skill GetSkillForBeast(string skillName)
    {
        return GetOneSkillFromFile(skillName, _pathBeastSkills);
    }
    private List<Skill> GetSkillsFromFile(List<string> namesToFind, string jsonPath)
    {
        var result = new List<Skill>();
        foreach (var name in namesToFind)
        {
            Skill match = GetOneSkillFromFile(name, jsonPath);
            if (match == null)
            {
                return null;
            }
            result.Add(match);
        }
        return result;
    }
    public Skill GetOneSkillFromFile(string skillName, string jsonPath)
    {
        var loader = new JsonInfoLoader();
        List<Skill> allAvailableSkills = loader.LoaderSkillsBd(jsonPath);
        if (allAvailableSkills == null) return null;
        return allAvailableSkills.FirstOrDefault(s => s.Name == skillName);
    }
}