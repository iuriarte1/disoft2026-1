using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class AllyOptionManager
{
    private View _view;
    private List<Traveler> _playerTeam;
    private string _actorName;
    private Skill _skill;
    private List<string> _namesOfAllies;
    private Traveler _allyChoosen;

    public AllyOptionManager(View view, List<Traveler> playerTeam, string actorName, Skill skill)
    {
        _view = view;
        _playerTeam = playerTeam;
        _actorName = actorName;
        _skill = skill;
        _namesOfAllies = new List<string>();
    }

    private bool IsReviveSkill() => _skill.Name == "Vivify";

    private List<Traveler> GetEligibleAllies()
    {
        return IsReviveSkill()
            ? _playerTeam.Where(t => t.IsDead).ToList()
            : _playerTeam.Where(t => !t.IsDead).ToList();
    }

    private void SetNamesOfAllies(List<Traveler> eligibleAllies)
    {
        foreach (var ally in eligibleAllies)
            _namesOfAllies.Add(ally.GetStatsSummary());
    }
    
    private void GetAllyChoosenFromConsole(List<Traveler> eligibleAllies)
    {
        int option = _view.GetAllyChoosen(_namesOfAllies, _actorName);
        if (option > _namesOfAllies.Count)
        {
            _allyChoosen = null;
            return;
        }
        _allyChoosen = eligibleAllies[option - 1];
    }

    public Traveler GetAllyChoosen()
    {
        var eligibleAllies = GetEligibleAllies();
        SetNamesOfAllies(eligibleAllies);
        GetAllyChoosenFromConsole(eligibleAllies);
        return _allyChoosen;
    }
}