using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Data;

public class TeamValidationService
{
    private View _view;
    private readonly int _activeSkillsPerTraveler = 8;
    private readonly int _passiveSkillsPerTraveler = 4;
    private int _passiveSkillsPerBeast = 4;
    private readonly int _maxTravelersPerTeam = 4;
    private readonly int _maxBeastsPerTeam = 5;

    public TeamValidationService(View view)
    {
        _view = view;
    }

    public bool ValidateTravelers(List<Traveler> travelers)
    {
        if (travelers == null) return true;
        if (!ValidateTravelersQuantitysInPlayerTeam(travelers)) return false;
        if (!ValidateActiveSkillsQuantityTravelers(travelers)) return false;
        if (!ValidatePassiveSkillsQuantityTravelers(travelers)) return false;
        if (!ValidateNotRepeatedTravelersInPlayerTeam(travelers)) return false;
        if (!ValidateNotRepeatedSkillsTravelers(travelers)) return false;
        return true;
    }

    public bool ValidateBeasts(List<Beast> beasts)
    {
        if (beasts == null) return true;
        if (!ValidateBeastsQuantitysInEnemy(beasts)) return false;
        if (!ValidateNotRepeatedBeastsInEnemy(beasts)) return false;
        return true;
    }
    private bool ValidateBeastsQuantitysInEnemy(List<Beast> enemy)
    {
        if (enemy == null) return true;
        if (enemy.Count > _maxBeastsPerTeam || enemy.Count < 1)
        {
            _view.InvalidTeamsFileMessage();
            return false;
        }
        return true;
    }
    private bool ValidateTravelersQuantitysInPlayerTeam(List<Traveler> travelers)
    {
        if (travelers == null) return true;
        if (travelers.Count > _maxTravelersPerTeam || travelers.Count < 1)
        {
            _view.InvalidTeamsFileMessage();
            return false;
        }
        return true;
    }
    private bool ValidateActiveSkillsQuantityTravelers(List<Traveler> travelers)
    {
        foreach (var traveler in travelers)
        {
            if (traveler.ActiveSkills != null && traveler.ActiveSkills.Count > _activeSkillsPerTraveler)
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
        }
        return true;
    }
    private bool ValidatePassiveSkillsQuantityTravelers(List<Traveler> travelers)
    {
        foreach (var traveler in travelers)
        {
            if (traveler.PasiveSkills != null && traveler.PasiveSkills.Count > _passiveSkillsPerTraveler)
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
        }
        return true;
    }
    private bool ValidateNotRepeatedTravelersInPlayerTeam(List<Traveler> travelers)
    {
        var names = new HashSet<string>();
        foreach (var t in travelers)
        {
            if (names.Contains(t.Name))
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
            names.Add(t.Name);
        }
        return true;
    }
    private bool ValidateNotRepeatedBeastsInEnemy(List<Beast> beasts)
    {
        var names = new HashSet<string>();
        foreach (var b in beasts)
        {
            if (names.Contains(b.Name))
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
            names.Add(b.Name);
        }
        return true;
    }
    private bool ValidateNotRepeatedSkillsTravelers(List<Traveler> travelers)
    {
        foreach (var traveler in travelers)
        {
            if (!ValidateNotRepeatedSkills(traveler.ActiveSkills)) return false;
            if (!ValidateNotRepeatedSkills(traveler.PasiveSkills)) return false;
        }
        return true;
    }
    private bool ValidateNotRepeatedSkills(List<Skill> skills)
    {
        if (skills == null || skills.Count == 0) return true;
        var names = new HashSet<string>();
        foreach (var s in skills)
        {
            if (s == null) continue;
            if (names.Contains(s.Name))
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
            names.Add(s.Name);
        }
        return true;
    }
}
