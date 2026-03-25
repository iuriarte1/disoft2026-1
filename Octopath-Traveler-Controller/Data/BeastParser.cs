using Octopath_Traveler_Model;
using Octopath_Traveler_View;
namespace Octopath_Traveler.Data;

public class BeastParser
{
    private readonly List<Beast> _beastsDatabase;
    private readonly View _view;
    public BeastParser(View vista, List<Beast> beastsDatabase)
    {
        _view = vista;
        _beastsDatabase = beastsDatabase;
    }
    public List<Beast> ParseTeam(List<string> names)
    {
        List<Beast> team = new();
        // Si las bestias también tienen habilidades en el JSON, 
        // podrías instanciar el SkillParser aquí también.
        var skillParser = new SkillParser(new List<string>());

        foreach (string name in names)
        {
            // Buscamos la bestia en la base de datos por su nombre
            Beast template = _beastsDatabase.FirstOrDefault(b => b.Name == name);

            if (template == null)
            {
                _view.InvalidTeamsFileMessage();
                return null;
            }

            template.Skill = skillParser.GetSkillForBeast(template.SkillName);
            Beast newBeast = CloneBeast(template);
            
            // Si el día de mañana las bestias tienen habilidades en el .txt 
            // como los viajeros, aquí llamarías al skillParser.
            
            team.Add(newBeast);
        }

        return team;
    }
    private Beast CloneBeast(Beast template)
    {
        // Vital: Creamos una instancia nueva para que si hay dos "Wolf", 
        // cada uno tenga su propia vida independiente en el combate.
        return new Beast
        {
            Name = template.Name,
            BaseStats = template.BaseStats,
            Weaknesses = template.Weaknesses,
            Skill = template.Skill,
            SkillName = template.SkillName,
            CurrentHp = template.BaseStats.MaxHp, // Inicia con vida llena
            CurrentSp = template.BaseStats.MaxSp, // Inicia con SP lleno
            Shields = template.Shields,
            // Weapons = template.Weapons (Si tus bestias usan armas)
        };
    }
}