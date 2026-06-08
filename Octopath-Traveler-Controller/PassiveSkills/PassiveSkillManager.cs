using Octopath_Traveler_Model;

namespace Octopath_Traveler.PassiveSkills;

public class PassiveSkillManager
{
    private readonly Dictionary<Traveler, List<IPassiveSkillEffect>> _effects;

    public PassiveSkillManager(List<Traveler> playerTeam)
    {
        _effects = playerTeam.ToDictionary(
            traveler => traveler,
            traveler => traveler.PassiveSkills
                .Select(skill => PassiveSkillFactory.Create(skill))
                .ToList()
        );
    }

    public void ApplyBattleStartEffects()
    {
        foreach (var (traveler, effects) in _effects)
        foreach (var effect in effects)
            effect.OnBattleStart(traveler);
    }

    public void ApplyEndOfRoundEffects()
    {
        foreach (var (traveler, effects) in _effects.Where(e => !e.Key.IsDead))
        foreach (var effect in effects)
            effect.OnEndOfRound(traveler);
    }

    public void ApplyOnHealEffects(Traveler owner, ref int healing)
    {
        if (!_effects.TryGetValue(owner, out var effects)) return;
        foreach (var effect in effects)
            effect.OnHeal(owner, ref healing);
    }

    public void ApplyOnDeathEffects(Traveler owner)
    {
        if (!_effects.TryGetValue(owner, out var effects)) return;
        foreach (var effect in effects)
            effect.OnDeath(owner);
    }

    public void ApplyOnBasicAttackEffects(Traveler owner, int totalDamage)
    {
        if (!_effects.TryGetValue(owner, out var effects)) return;
        foreach (var effect in effects)
            effect.OnBasicAttack(owner, totalDamage);
    }
}