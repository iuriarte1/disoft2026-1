using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class WeaponOptionManager
{
    private readonly Traveler _actor;
    private readonly View _view;

    public WeaponOptionManager(View view, Traveler actor)
    {
        _view = view;
        _actor = actor;
    }

    public string GetWeaponChosen()
    {
        var weaponNames = _actor.Weapons.ToList();
        int index = _view.GetWeaponOption(weaponNames);
        if (index > weaponNames.Count)
            return null;
        return weaponNames[index - 1];
    }
}