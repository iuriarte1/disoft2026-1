using Octopath_Traveler_Model;
using Octopath_Traveler_View;
namespace Octopath_Traveler.Data;

public class BeastParser
{
    private readonly List<Beast> _beastsDatabase;
    private readonly View _view;
    public BeastParser(View view, List<Beast> beastsDatabase)
    {
        _view = view;
        _beastsDatabase = beastsDatabase;
    }
    public List<Beast> ParseEnemyTeam(List<string> names)
    {
        List<Beast> team = new();
        var skillParser = new SkillParser(new List<string>());
        foreach (string name in names)
        {
            Beast template = _beastsDatabase.FirstOrDefault(b => b.Name == name);
            if (template == null)
            {
                _view.InvalidTeamsFileMessage();
                return null;
            }
            template.Skill = skillParser.GetSkillForBeast(template.SkillName);
            Beast newBeast = new Beast(template);
            team.Add(newBeast);
        }
        return team;
    }
}