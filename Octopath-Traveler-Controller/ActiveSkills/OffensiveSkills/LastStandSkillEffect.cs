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
        var (rawDamage, enteredBreakingPoint) = ActiveSkillDamageCalculator.CalculateRawDamageAndShieldReduction(atacante, victim, _skill);
        int missingHpPercent = (int)Math.Floor((double)(atacante.BaseStats.MaxHp - atacante.CurrentHp) / atacante.BaseStats.MaxHp * 100);
        double bonus = 1 + (missingHpPercent * 3.0 / 100);
        int finalDamage = Convert.ToInt32(Math.Floor(rawDamage * bonus));
        return (Math.Max(finalDamage, 0), enteredBreakingPoint);
    }
}