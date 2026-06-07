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

    private readonly int _hits;

    public DamageManager(Traveler actor, Beast victim, string weapon, View view, int hits = 1)
    {
        _actor = actor;
        _victim = victim;
        _view = view;
        _weapon = weapon;
        _hits = hits;
    }

    public void Execute()
    {
        _view.ShowAttackerAnnouncement(_actor.Name);
        for (int i = 0; i < _hits; i++)
        {
            var (damage, enteredBreakingPoint) = DamageCalculator.Calculate(
                _actor, _victim, _weapon, BasicAttackModifier);
            _victim.TakeDamage(damage);
            ShowHitMessage(damage);
            if (enteredBreakingPoint)
                _view.ShowBreakingPoint(_victim.Name);
        }
        _view.ShowFinalHp(_victim.Name, _victim.CurrentHp);
    }

    private void ShowHitMessage(int damage)
    {
        var result = new BasicAttackResult(_actor.Name, _victim.Name, _weapon, damage);
        bool isWeakness = _victim.Weaknesses.Contains(_weapon);
        if (isWeakness)
            _view.ShowBasicAttackHitWithWeaknessMessage(result);
        else
            _view.ShowBasicAttackHitMessage(result);
    }
}