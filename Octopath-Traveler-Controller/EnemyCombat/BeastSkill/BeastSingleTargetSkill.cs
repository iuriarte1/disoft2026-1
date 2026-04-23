using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastSingleTargetSkill : IBeastSkillEffect
{
    private static Skill _skill;
    private readonly IVictimSelector _victimSelector;
    private readonly string _skillType;
    
    public BeastSingleTargetSkill(Skill skill,string skilltype,  IVictimSelector victimSelector)
    {
        _skill = skill;
        _victimSelector = victimSelector;
        _skillType = skilltype;
    }

    private string GetSkillTypeNameForMessage()
    {
        return _skillType == "Phys" ? "físico" : "elemental";
    }
    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        var victim = _victimSelector.SelectVictim(aliveTeam);
        int damage = CalculateDamage(actor, victim);
        victim.TakeDamage(damage);
        view.ShowBeastAtack(actor.Name, victim.Name, damage, _skill.Name, victim.CurrentHp, GetSkillTypeNameForMessage());
    }

    private int CalculateDamage(Beast actor, Traveler victim)
    {
        double raw = _skillType == "Phys"
            ? actor.BaseStats.PhysicalAttack * _skill.Modifier - victim.BaseStats.PhysicalDefense
            : actor.BaseStats.ElementalAttack * _skill.Modifier - victim.BaseStats.ElementalDefense;
        return Convert.ToInt32(Math.Floor(Math.Max(raw, 0)));
    }
}