using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.BeastSkill;

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
    private List<Traveler> _playerTeam;
    public EnemyTurn(Beast actor,List<Traveler> team, View view)
    {
        _actor = actor;
        _view = view;
        _playerTeam = team;
    }
    public void Execute()
    {
        new BeastSkillFactory().Create(_actor.Skill).Execute(_actor, _playerTeam, _view);
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