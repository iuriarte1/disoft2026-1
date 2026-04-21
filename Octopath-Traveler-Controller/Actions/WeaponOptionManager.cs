
using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class WeaponOptionManager
{
    private Traveler _actor;
    private View _view;
    private List<string> _weaponsNames;
    private string _weaponChoosenName;
    private int _weaponChoosenIndex;
    private bool _choosingWeaponForSkill;
    public WeaponOptionManager(View view, Traveler actor, bool weaponForSkill = false)
    {
        _view = view;
        _actor = actor;
        _weaponsNames = new List<string>();
        _choosingWeaponForSkill = weaponForSkill;
    }
    private void GetWeaponsNamesFormActor()
    {
        foreach (string weapon in _actor.Weapons)
        {
            _weaponsNames.Add(weapon);
        }
    }

    private void GetWeaponsForSkillUse()
    {
        foreach (AllWeaponsTypes weapon in Enum.GetValues<AllWeaponsTypes>())
        {
            _weaponsNames.Add(weapon.ToString());
        }
    }
    
    private void GetIndexWeaponChoosenFromConsole()
    {
        _weaponChoosenIndex = _view.GetWeaponOption(_weaponsNames);
    }
    private void GetWeaponChoosenNameFromIndex()
    {
        if (_weaponChoosenIndex > _weaponsNames.Count)
        {
            _weaponChoosenName = null;
            return;
        }
        _weaponChoosenName = _weaponsNames[_weaponChoosenIndex - 1];
    }
    public string GetWeaponChoosen()
    {
        if (_choosingWeaponForSkill)
        {
            GetWeaponsForSkillUse();
        }
        else
        {
            GetWeaponsNamesFormActor();
        }
        GetIndexWeaponChoosenFromConsole();
        GetWeaponChoosenNameFromIndex();
        return _weaponChoosenName;
    }
}