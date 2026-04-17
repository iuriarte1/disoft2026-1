using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class VictimOptionManager
{
    private View _view;
    private List<Beast> _enemyTeam;
    private string _travelerName;
    private List<string> _namesOfBeasts;
    private List<string> _beastsStats;
    private string _nameVictimChoosen;
    private Beast _victimChoosen;

    public VictimOptionManager(View view, List<Beast> enemyTeam, string travelerName)
    {
        _view = view;
        _enemyTeam = enemyTeam;
        _travelerName = travelerName;
        _namesOfBeasts = new List<string>();
        _beastsStats = new List<string>();
    }
    private void SetNamesOfBeasts()
    {
        foreach (var beast in _enemyTeam.Where(b => !b.IsDead))
        {
            _namesOfBeasts.Add(beast.Name);
        }
    }
    private void SetBeastsStatsForMessage()
    {
        foreach (var beast in _enemyTeam.Where(b => !b.IsDead))
        {
            _beastsStats.Add(beast.GetStatsSummary());
        }
    }
    private void SetVictimsData()
    {
        SetNamesOfBeasts();
        SetBeastsStatsForMessage();
    }
    private void ShowVictimsOptionMessageConsole()
    {
        SetVictimsData();
        _view.ShowVictimsAvailableMessageForTraveler(_beastsStats, _travelerName);
    }
    private void GetVictimChoosenFromConsole()
    {
        int option = _view.GetVictimOptionForTraveler();
        if (IfVictimsOptionIsCancel(option))
        {
            OptionIsCancelar();
            return;
        }
        _nameVictimChoosen = _namesOfBeasts[option - 1];
        GetVictimChoosenFromBeastName();
    }
    private bool IfVictimsOptionIsCancel(int option)
    {
        if (option > _namesOfBeasts.Count)
        {
            return true;
        }
        return false;
    }
    private void OptionIsCancelar()
    {
        _victimChoosen = null;
    }
    private void GetVictimChoosenFromBeastName()
    {
        _victimChoosen = _enemyTeam.First(beast => beast.Name == _nameVictimChoosen);
    }
    public Beast GetVictimChoosen()
    {
        ShowVictimsOptionMessageConsole();
        GetVictimChoosenFromConsole();
        return _victimChoosen;
    }
}