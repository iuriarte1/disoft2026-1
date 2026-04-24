using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class AllWeaponsOptionManager
{
    private readonly View _view;

    public AllWeaponsOptionManager(View view)
    {
        _view = view;
    }

    public string GetWeaponChosen()
    {
        var weaponNames = Enum.GetValues<AllWeaponsTypes>()
            .Select(w => w.ToString())
            .ToList();
        int index = _view.GetWeaponOption(weaponNames);
        if (index > weaponNames.Count)
            return null;
        return weaponNames[index - 1];
    }
}