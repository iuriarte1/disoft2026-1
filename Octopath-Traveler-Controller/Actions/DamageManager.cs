using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class DamageManager
{
    private Beast _victim;
    private Traveler _actor;
    private int _damage;
    private double _basicAttackModifier = 1.3;
    private View _view;
    private string _weapon;
    
    public DamageManager( Traveler actor, Beast victim, string weapon, View view)
    {
        _actor = actor;
        _victim = victim;
        _view = view;
        _weapon = weapon;
    }
    private void DamageCalculation()
    {
        double damageDecimal =  Math.Floor((double)_actor.BaseStats.PhysicalAttack * _basicAttackModifier - (double)_victim.BaseStats.PhysicalDefense);
        _damage = Convert.ToInt32(damageDecimal);
    }
    private void DamageValidator()
    {
        if (_damage < 0)
        {
            _damage = 0;
        }
    }
    private void VictimTakeDamage()
    {
        _victim.TakeDamage(_damage);
    }
    private void ShowDamageResultMessageInConsole()
    {
        _view.ShowBasicAttackResultMessage(_actor.Name, _victim.Name, _weapon, _damage, _victim.CurrentHp);
    }
    public void Execute()
    {
        DamageCalculation();
        DamageValidator();
        VictimTakeDamage();
        ShowDamageResultMessageInConsole();
    }
}