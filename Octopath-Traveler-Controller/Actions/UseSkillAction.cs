using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.ActiveSkills;

namespace Octopath_Traveler.Actions;

public class UseSkillAction : ICombatAction
{
    private List<string> _skillsNames = new List<string>();
    private Traveler _actor;
    private int _indexOfSkillChosen;
    private View _view;
    private Skill _skillChosen;
    private List<Beast> _enemyTeam;
    private List<Traveler> _playerTeam;
    private Beast _victimChosen;
    private Traveler _allyChosen;
    private int _bPToUse;
    
    public bool Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        _actor = actor;
        _view = view;
        _enemyTeam = enemyTeam;
        _playerTeam = playerTeam;
        SaveSkillsNames();
        GetIndexSkillChosen();
        if (!ValidateIndexSkillChosen())
        {
            return false;
        }
        GetSkillChosen();
        if (!SelectTarget()) 
        {
            return false;
        }
        GetBoostPointsToUse();
        ChangeSpTravelerFromSkillCost();
        ExecuteSkillEffect();
        return true;
    }
    //bypaseado para la E2

    private void SaveSkillsNames()
    {
        foreach (var skill in _actor.ActiveSkills.Where(s=> s.SP <= _actor.CurrentSp))
        {
            _skillsNames.Add(skill.Name);
        }
    }

    private void GetBoostPointsToUse()
    {
        if (SkillSelectsWeaponFirst()) return;
        _bPToUse = _view.GetHowManyBoostPointsToUse();
    }
    private void GetIndexSkillChosen()
    {
        _indexOfSkillChosen = _view.GetSkillOptionChoosen(_skillsNames, _actor.Name);
    }
    private bool ValidateIndexSkillChosen()
    {
        if (_indexOfSkillChosen > _skillsNames.Count)
        {
            return false;
        }
        return true;
    }

    private void GetSkillChosen()
    {
        _skillChosen = _actor.ActiveSkills[_indexOfSkillChosen - 1];
    }

    private void ChangeSpTravelerFromSkillCost()
    {
        _actor.CurrentSp -= _skillChosen.SP;
    }
    private bool SelectTarget()
    {
        switch (_skillChosen.Target)
        {
            case "Single":
                if (SkillSelectsWeaponFirst()) return true;
                _victimChosen = new VictimOptionManager(_view, _enemyTeam, _actor.Name).GetVictimChoosen();
                return _victimChosen != null;
            //case "Ally":
            //    _allyChosen = new AllyOptionManager(_view, _playerTeam, _actor.Name).GetAllyChoosen();
            //    return _allyChosen != null;
            case "Enemies":
            case "Party":
            case "User":
                return true;
            default:
                return false;
        }
    }
    private void ExecuteSkillEffect()
    {
        IActiveSkillEffect effect = SkillEffectFactory.Create(_skillChosen, _actor, _victimChosen, _allyChosen);
        effect.Execute(_actor, _playerTeam, _enemyTeam, _view);
    }
    private bool SkillSelectsWeaponFirst()
        => _skillChosen.Name == "Nightmare Chimera";
}
