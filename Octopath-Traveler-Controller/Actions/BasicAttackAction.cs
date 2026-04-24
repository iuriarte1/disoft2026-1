using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class BasicAttackAction : ICombatAction
{
    private string _weaponChoosen;
    private Beast _victimChoosen;
    private int _boostPointsToUse;
    public bool Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        var livingEnemies = enemyTeam.Where(e => !e.IsDead).ToList();
        GetWeaponToAttack(actor, view);
        if (WeaponOptionsIsCancel())
        {
            return false;
        }
        ChooseVictimToAttack(livingEnemies, view, actor); 
        if (VictimOptionsIsCancel())
        {
            return false; 
        }
        GetBpToUse(view);
        DamageCalculatorFromWeapon(actor, view);
        return true;
    }
    private void GetWeaponToAttack(Traveler actor, View view)
    {
        _weaponChoosen = new WeaponOptionManager(view, actor).GetWeaponChosen();
    }
    private bool WeaponOptionsIsCancel()
    {
        if (_weaponChoosen == null)
        {
            return true;
        }
        return false;
    }
    private void ChooseVictimToAttack(List<Beast> enemyTeam, View view, Traveler actor)
    {
        var victimOptionManager = new VictimOptionManager(view, enemyTeam, actor.Name);
        _victimChoosen = victimOptionManager.GetVictimChoosen();
    }
    private bool VictimOptionsIsCancel()
    {
        if (_victimChoosen == null)
        {
            return true;
        }
        return false;
    }
    private void GetBpToUse(View view)
    {
        _boostPointsToUse = view.GetHowManyBoostPointsToUse();
    }
    private void DamageCalculatorFromWeapon(Traveler actor, View view)
    {
        var damageManager = new DamageManager(actor, _victimChoosen, _weaponChoosen, view);
        damageManager.Execute();
    }
}