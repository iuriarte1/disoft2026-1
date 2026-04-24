using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class LastStandSkillEffect : AllEnemiesOffensiveSkill
{
    private Traveler _atacante;

    public LastStandSkillEffect(Traveler atacante, Skill skill) : base(skill)
    {
        _atacante = atacante;
    }

    protected override (int damage, bool enteredBreakingPoint) CalculateDamage(Traveler atacante, Beast victim)
    {
        double missingHpPercent = Math.Floor(
            (double)(atacante.BaseStats.MaxHp - atacante.CurrentHp) / atacante.BaseStats.MaxHp * 100);
        double lastStandBonus = 1 + (missingHpPercent * 3.0 / 100);

        double baseDamage = DamageCalculator.GetBaseDamage(atacante, victim, _skill.Type, _skill.Modifier);
        double damageWithBonus = baseDamage * lastStandBonus;

        double multiplier = DamageCalculator.GetDamageMultiplier(victim, _skill.Type);
        bool enteredBreakingPoint = ShieldManager.TryReduceShield(victim, _skill.Type, damageWithBonus);
        int finalDamage = Convert.ToInt32(Math.Floor(damageWithBonus * multiplier));

        return (Math.Max(finalDamage, 0), enteredBreakingPoint);
    }
}