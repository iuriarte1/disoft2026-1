using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class HpThiefSkillEffect : OffensiveSkillEffect
{
    private readonly Beast _victim;

    public HpThiefSkillEffect(Skill skill, Beast victim) : base(skill)
    {
        _victim = victim;
    }

    protected override List<Beast> SelectVictims(List<Beast> enemyTeam)
        => new List<Beast> { _victim };

    public override void Execute(Traveler attacker, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(attacker.Name, _skill.Name);
        int totalDamage = 0;
        for (int i = 0; i < _skill.Hits; i++)
        {
            var (damage, enteredBreakingPoint) = CalculateDamage(attacker, _victim);
            _victim.TakeDamage(damage);
            totalDamage += damage;
            ShowDamageMessage(_victim, damage, view);
            if (enteredBreakingPoint)
                view.ShowBreakingPoint(_victim.Name);
        }
        int healing = totalDamage / 2;
        attacker.RestoreHp(healing);
        view.ShowHealingResult(attacker.Name, healing);
        view.ShowFinalHp(_victim.Name, _victim.CurrentHp);
        view.ShowFinalHp(attacker.Name, attacker.CurrentHp);
    }

    private void ShowDamageMessage(Beast victim, int damage, View view)
    {
        bool isWeakness = victim.Weaknesses.Contains(_skill.Type);
        if (isWeakness)
            view.ShowSkillDamageResultWithWeakness(victim.Name, _skill.Type, damage);
        else
            view.ShowSkillDamageResult(victim.Name, _skill.Type, damage);
    }
}