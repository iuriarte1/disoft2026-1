using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Data;

public class TeamValidationService
{
    private View _view;
    private int ActiveSkillsPerTraveler = 8;
    private int PassiveSkillsPerTraveler = 4;
    private int PassiveSkillsPerBeast = 4;
    private int MaxTravelersPerTeam = 4;
    private int MaxBeastsPerTeam = 5;

    public TeamValidationService(View view)
    {
        _view = view;
    }

    public bool ValidateTravelers(List<Traveler> travelers)
    {
        if (travelers == null) return true;
        if (!ValidateTravelersQuantity(travelers)) return false;
        if (!ValidateActiveSkillsTravelers(travelers)) return false;
        if (!ValidatePassiveSkillsTravelers(travelers)) return false;
        if (!ValidateNotRepeatedTravelers(travelers)) return false;
        if (!ValidateNotRepeatedSkillsTravelers(travelers)) return false;
        return true;
    }

    public bool ValidateBeasts(List<Beast> beasts)
    {
        if (beasts == null) return true;
        if (!ValidateBeastsQuantity(beasts)) return false;
        if (!ValidateSkillsBeasts(beasts)) return false;
        if (!ValidateNotRepeatedBeasts(beasts)) return false;
        return true;
    }

    private bool SkipTraveler(Traveler traveler) => traveler == null;
    private bool SkipBeast(Beast beast) => beast == null;

    private bool ValidateBeastsQuantity(List<Beast> beasts)
    {
        if (beasts == null) return true;
        if (beasts.Count > MaxBeastsPerTeam || beasts.Count < 1)
        {
            _view.InvalidTeamsFileMessage();
            return false;
        }
        return true;
    }

    private bool ValidateTravelersQuantity(List<Traveler> travelers)
    {
        if (travelers == null) return true;
        if (travelers.Count > MaxTravelersPerTeam || travelers.Count < 1)
        {
            _view.InvalidTeamsFileMessage();
            return false;
        }
        return true;
    }

    private bool ValidateActiveSkillsTravelers(List<Traveler> travelers)
    {
        foreach (var traveler in travelers)
        {
            if (SkipTraveler(traveler)) continue;
            if (traveler.ActiveSkills != null && traveler.ActiveSkills.Count > ActiveSkillsPerTraveler)
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
        }
        return true;
    }

    private bool ValidatePassiveSkillsTravelers(List<Traveler> travelers)
    {
        foreach (var traveler in travelers)
        {
            if (SkipTraveler(traveler)) continue;
            if (traveler.PasiveSkills != null && traveler.PasiveSkills.Count > PassiveSkillsPerTraveler)
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
        }
        return true;
    }

    private bool ValidateSkillsBeasts(List<Beast> beasts)
    {
        if (beasts == null) return true;
        foreach (var beast in beasts)
        {
            if (SkipBeast(beast)) continue;
            if (beast.Skills != null && beast.Skills.Count > PassiveSkillsPerBeast)
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
        }
        return true;
    }

    private bool ValidateNotRepeatedTravelers(List<Traveler> travelers)
    {
        var names = new HashSet<string>();
        foreach (var t in travelers)
        {
            if (SkipTraveler(t)) continue;
            if (names.Contains(t.Name))
            {
                _view.InvalidTeamsFileMessage();
                return false;
            }
            names.Add(t.Name);
        }
        return true;
    }

    private bool ValidateNotRepeatedBeasts(List<Beast> beasts)
    {
        var names = new HashSet<string>();
        foreach (var b in beasts)
        {
            if (SkipBeast(b)) continue;
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
            if (SkipTraveler(traveler)) continue;
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
