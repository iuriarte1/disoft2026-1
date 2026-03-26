using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class EnemyTurn
{
    private Beast _actor;
    private View _view;
    private Traveler _victimHighestHp;
    private int _damage;
    // harcodeadooo
    private double _basicAttackModifier = 1.3;
    private List<Traveler> _travelersAlive;
    private List<Traveler> _travelers;
    public EnemyTurn(Beast actor,List<Traveler> team, View view)
    {
        _actor = actor;
        _view = view;
        _travelers = team;
    }
    public void Execute()
    {
        GetTravelerAlive(_travelers);
        ChooseVictimHighestHp(_travelersAlive);
        GetDamageFromAttack();
        DamageValidator();
        _victimHighestHp.TakeDamage(_damage);
        _view.ShowBeastAtack(_actor.Name, _victimHighestHp.Name, _damage, "Attack", _victimHighestHp.CurrentHp);
    }

    private void GetTravelerAlive(List<Traveler> travelers)
    {
        _travelersAlive= travelers.Where(t => !t.IsDead).ToList();
    }
    private void ChooseVictimHighestHp(List<Traveler> travelersAlive)
    {
        _victimHighestHp = travelersAlive.OrderByDescending(t => t.CurrentHp).First();
    }

    private void GetDamageFromAttack()
    {
        double damageDecimal =  Math.Floor((double)_actor.BaseStats.PhysicalAttack * _basicAttackModifier - (double)_victimHighestHp.BaseStats.PhysicalDefense);
        _damage = Convert.ToInt32(damageDecimal);
    }

    private void DamageValidator()
    {
        if (_damage < 0)
        {
            _damage = 0;
        }
    }
}