using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class VortalClawSkillEffect : IBeastSkillEffect
{
    private readonly Skill _skill;

    public VortalClawSkillEffect(Skill skill)
    {
        _skill = skill;
    }

    public void Execute(Beast actor, List<Traveler> playerTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        var aliveTeam = playerTeam.Where(t => !t.IsDead).ToList();
        foreach (var victim in aliveTeam)
        {
            int targetHp = (int)Math.Floor(victim.CurrentHp / 2.0);
            int damage = victim.CurrentHp - targetHp;
            victim.TakeDamage(damage);
            view.ShowBeastDamageWithVortalClaw(victim.Name, damage);
        }
        ShowFinalHpVicitms(aliveTeam, view);
    }
    private void ShowFinalHpVicitms(List<Traveler> aliveInTeam, View view)
    {
        foreach (var victim in aliveInTeam)
        {
            view.ShowFinalHp(victim.Name, victim.CurrentHp);
        }
    }
}