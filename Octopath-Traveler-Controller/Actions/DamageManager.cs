using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class DamageManager
{
    private Beast _victim;
    private Traveler _actor;
    private View _view;
    private string _weapon;
    private const double BasicAttackModifier = 1.3;

    public DamageManager(Traveler actor, Beast victim, string weapon, View view)
    {
        _actor = actor;
        _victim = victim;
        _view = view;
        _weapon = weapon;
    }

    public void Execute()
    {
        var (damage, enteredBreakingPoint) = DamageCalculator.Calculate(
            _actor, _victim, _weapon, BasicAttackModifier);
        _victim.TakeDamage(damage);
        ShowDamageMessage(damage);
        if (enteredBreakingPoint)
            _view.ShowBreakingPoint(_victim.Name);
        
    }

    private void ShowDamageMessage(int damage)
    {
        bool isWeakness = _victim.Weaknesses.Contains(_weapon);
        if (isWeakness)
            _view.ShowBasicAttackWithWeaknessResultMessage(_actor.Name, _victim.Name, _weapon, damage, _victim.CurrentHp);
        else
            _view.ShowBasicAttackResultMessage(_actor.Name, _victim.Name, _weapon, damage, _victim.CurrentHp);
    }
}