using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class UseSkillAction : ICombatAction
{
    private List<string> _skillsNames = new List<string>();
    private Traveler _actor;
    private int _indexOfSkillChosen;
    private View _view;
    
    public bool Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        _actor = actor;
        _view = view;
        SaveSkillsNames();
        GetSkillChosen();
        if (!ValidateSkillChosen())
        {
            return false;
        }
        return true;
    }
    //bypaseado para la E2
    private void GetSkillsAvailable(Traveler traveler)
    {
        _skillsNames = traveler.ActiveSkills.Select(s => s.Name).ToList();
    }
    private void SaveSkillsNames()
    {
        foreach (var skill in _actor.ActiveSkills)
        {
            _skillsNames.Add(skill.Name);
        }
    }
    private void GetSkillChosen()
    {
        _indexOfSkillChosen = _view.GetSkillOptionChoosen(_skillsNames, _actor.Name);
    }
    private bool ValidateSkillChosen()
    {
        if (_indexOfSkillChosen > _skillsNames.Count)
        {
            return false;
        }
        return true;
    }
}
