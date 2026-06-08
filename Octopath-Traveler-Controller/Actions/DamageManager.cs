using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.PassiveSkills;

namespace Octopath_Traveler.Actions;

public class DamageManager
{
    private readonly Traveler _actor;
    private readonly Beast _victim;
    private readonly View _view;
    private readonly string _weapon;
    private readonly PassiveSkillManager _passiveManager;
    private readonly int _hits;
    private const double BasicAttackModifier = 1.3;

    public DamageManager(Traveler actor, Beast victim, string weapon, View view, PassiveSkillManager passiveManager, int hits = 1)
    {
        _actor = actor;
        _victim = victim;
        _view = view;
        _weapon = weapon;
        _passiveManager = passiveManager;
        _hits = hits;
    }

    public void Execute()
    {
        _view.ShowAttackerAnnouncement(_actor.Name);
        int totalDamage = 0;
        for (int i = 0; i < _hits; i++)
        {
            var (damage, enteredBreakingPoint) = DamageCalculator.Calculate(
                _actor, _victim, _weapon, BasicAttackModifier);
            _victim.TakeDamage(damage);
            totalDamage += damage;
            ShowHitMessage(damage);
            if (enteredBreakingPoint)
                _view.ShowBreakingPoint(_victim.Name);
        }
        _passiveManager.ApplyOnBasicAttackEffects(_actor, totalDamage);
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