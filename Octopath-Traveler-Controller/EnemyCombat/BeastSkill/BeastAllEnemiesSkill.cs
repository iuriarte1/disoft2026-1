using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastAllEnemiesSkill : IBeastSkillEffect
{
    private static Skill _skill;
    private readonly string _skillType;
    public BeastAllEnemiesSkill(Skill skill, string skillType)
    {
        _skill = skill;
        _skillType = skillType;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        foreach (var victim in aliveTeam)
        {
            int damage = CalculateDamage(actor, victim);
            victim.TakeDamage(damage);
            view.ShowBeastDamage(victim.Name, damage, GetSkillTypeNameForMessage());
        }
        ShowFinalHpVicitms(aliveTeam, view);
    }
    private string GetSkillTypeNameForMessage()
    {
        return _skillType == "Phys" ? "físico" : "elemental";
    }
    private void ShowFinalHpVicitms(List<Traveler> aliveInTeam, View view)
    {
        foreach (var victim in aliveInTeam)
        {
            view.ShowFinalHp(victim.Name, victim.CurrentHp);
        }
    }
    private int CalculateDamage(Beast actor, Traveler victim)
    {
        double raw = _skillType == "Phys"
            ? actor.BaseStats.PhysicalAttack * _skill.Modifier - victim.BaseStats.PhysicalDefense
            : actor.BaseStats.ElementalAttack * _skill.Modifier - victim.BaseStats.ElementalDefense;
        return Convert.ToInt32(Math.Floor(Math.Max(raw, 0)));
    }
}