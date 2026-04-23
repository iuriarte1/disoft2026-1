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
        ApplyDamageToTeam(actor, aliveTeam, view);
        ShowFinalHpOfTeam(aliveTeam, view);
    }
    private void ApplyDamageToTeam(Beast actor, List<Traveler> aliveTeam, View view)
    {
        foreach (var victim in aliveTeam)
            ApplyDamageToVictim(actor, victim, view);
    }

    private void ApplyDamageToVictim(Beast actor, Traveler victim, View view)
    {
        if (victim.IsDefendingThisRound)
            view.ShowTravelerDefending(victim.Name);

        int damage = BeastDamageCalculator.Calculate(actor, victim, _skill, _skillType);
        victim.TakeDamage(damage);
        view.ShowBeastDamage(victim.Name, damage, GetAttackTypeName());
    }

    private string GetAttackTypeName()
    {
        return _skillType == "Phys" ? "físico" : "elemental";
    }

    private void ShowFinalHpOfTeam(List<Traveler> aliveTeam, View view)
    {
        foreach (var victim in aliveTeam)
            view.ShowFinalHp(victim.Name, victim.CurrentHp);
    }
}