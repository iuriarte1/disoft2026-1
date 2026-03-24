using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class TeamManager
{
    private List<Unit> _units;
    private List<string> _positionUnitInMessage = ["A","B","C","D","E"];
    public TeamManager(List<Unit>team)
    {
        _units = team;
    }

    public List<string> GetTeamCurrentStats()
    {
        List<string> teamStats = new List<string>();
        int positionIndexUnit = 0;
        foreach (var unit in _units)
        {
            teamStats.Add( _positionUnitInMessage[positionIndexUnit] + "-" + unit.GetStatsSummary());
        }
        return teamStats; 
    }
}