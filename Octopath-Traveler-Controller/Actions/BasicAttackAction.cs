using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.PassiveSkills;

namespace Octopath_Traveler.Actions;

public class BasicAttackAction : ICombatAction
{
    private string _weaponChoosen;
    private Beast _victimChoosen;
    private int _boostPointsToUse;

    public bool Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view, PassiveSkillManager passiveManager)
    {
        var livingEnemies = enemyTeam.Where(e => !e.IsDead).ToList();
        GetWeaponToAttack(actor, view);
        if (WeaponOptionsIsCancel())
            return false;
        ChooseVictimToAttack(livingEnemies, view, actor);
        if (VictimOptionsIsCancel())
            return false;
        GetBpToUse(view, actor);
        ExecuteAttack(actor, view, passiveManager);
        return true;
    }

    private void GetWeaponToAttack(Traveler actor, View view)
    {
        _weaponChoosen = new WeaponOptionManager(view, actor).GetWeaponChosen();
    }

    private bool WeaponOptionsIsCancel() => _weaponChoosen == null;

    private void ChooseVictimToAttack(List<Beast> enemyTeam, View view, Traveler actor)
    {
        _victimChoosen = new VictimOptionManager(view, enemyTeam, actor.Name).GetVictimChoosen();
    }

    private bool VictimOptionsIsCancel() => _victimChoosen == null;

    private void GetBpToUse(View view, Traveler actor)
    {
        _boostPointsToUse = new BpInputHandler(view).GetValidBoostPoints(actor);
        actor.SpendBp(_boostPointsToUse);
    }

    private void ExecuteAttack(Traveler actor, View view, PassiveSkillManager passiveManager)
    {
        int hits = 1 + _boostPointsToUse;
        var damageManager = new DamageManager(actor, _victimChoosen, _weaponChoosen, view, passiveManager, hits);
        damageManager.Execute();
    }
}