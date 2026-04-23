using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastSingleTargetSkill : IBeastSkillEffect
{
    private static Skill _skill;
    private readonly IVictimSelector _victimSelector;

    public BeastSingleTargetSkill(Skill skill, IVictimSelector victimSelector)
    {
        _skill = skill;
        _victimSelector = victimSelector;
    }
    private string GetAttackTypeName()
    {
        return _skill.Type == "Phys" ? "físico" : "elemental";
    }
    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        var victim = _victimSelector.SelectVictim(aliveTeam);
        int damage = CalculateDamage(actor, victim);
        victim.TakeDamage(damage);
        view.ShowBeastAtack(actor.Name, victim.Name, damage, _skill.Name, victim.CurrentHp, GetAttackTypeName());
    }

    private int CalculateDamage(Beast actor, Traveler victim)
    {
        double raw = _skill.Type == "Phys"
            ? actor.BaseStats.PhysicalAttack * _skill.Modifier - victim.BaseStats.PhysicalDefense
            : actor.BaseStats.ElementalAttack * _skill.Modifier - victim.BaseStats.ElementalDefense;
        return Convert.ToInt32(Math.Floor(Math.Max(raw, 0)));
    }
}