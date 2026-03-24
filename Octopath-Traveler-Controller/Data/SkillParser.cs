using Octopath_Traveler_Model;
namespace Octopath_Traveler.Data;

public class SkillParser
{
    private List<string> _skills;
    private string _pathActiveSkills = "data/skills.json";
    private string _pathBeastSkills = "data/beast_skills.json";
    private string _pathPassiveSkills = "data/passive_skills.json";
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
    public List<Skill> GetSkillsForBeast(List<string> skillNames)
    {
         return GetSkillsFromFile(skillNames, _pathBeastSkills);
    }
    private List<Skill> GetSkillsFromFile(List<string> namesToFind, string jsonPath)
    {
        var loader = new JsonInfoLoader();
        // Asumo que tu JsonInfoLoader tiene un método para cargar una lista de Skills
        List<Skill> allAvailableSkills = loader.LoaderSkills(jsonPath); 

        List<Skill> result = new();

        foreach (string name in namesToFind)
        {
            // Buscamos el objeto Skill que coincida con el nombre escrito en el .txt
            Skill match = allAvailableSkills.FirstOrDefault(s => s.Name == name);

            if (match == null)
            {
                // Si una habilidad no existe, devolvemos null para que el TeamBuilder
                // sepa que el archivo de equipos no es válido.
                return null; 
            }

            result.Add(match);
        }

        return result;
    }
}