using System.Text.Json.Serialization;
namespace Octopath_Traveler_Model;

public class Unit
{
    public string Name { get; set; }
    [JsonPropertyName("Stats")]
    public Stats BaseStats { get; set; }
    public int CurrentHp { get; set; }
    public int CurrentSp { get; set; }
    public int CurrentBp { get; set; }
    public bool IsDead => CurrentHp <= 0;
    public bool RevivedThisRound { get; set; } = false;
    public bool HasDefendPriorityNextRound { get; set; } = false;
    public bool HasDefendPriorityThisRound { get; set; } = false;
    public bool IsDefendingThisRound { get; set; } = false;
    public bool HasTurnPriorityFromSkill { get; set; } = false;
    public bool HasTurnPriorityThisRound { get; set; } = false;
    public int RoundsInLastTurn { get; set; } = 0;
    private readonly List<StatEffect> _statusEffects = new();
    public IReadOnlyList<StatEffect> StatusEffects => _statusEffects;
    private const double SpeedIncreasedFactor = 1.5;
    private const double SpeedDecreasedFactor = 2.0 / 3.0;
    
    public bool UsedBoostThisRound { get; private set; } = false;

    public void SpendBp(int amount)
    {
        CurrentBp -= amount;
        if (amount > 0) UsedBoostThisRound = true;
    }

    public void ResetBoostUsage() => UsedBoostThisRound = false;
    public void TakeDamage(int damageAmount)
    {
        CurrentHp -= damageAmount;
        if (CurrentHp < 0) CurrentHp = 0;
    }

    public void RestoreHp(int healing)
    {
        CurrentHp += healing;
        if (CurrentHp > BaseStats.MaxHp) CurrentHp = BaseStats.MaxHp;
    }

    public void Revive()
    {
        CurrentHp = 1;
    }

    public void SpendSp(int amount)
    {
        CurrentSp -= amount;
    }
    
    public void GainBp(int amount = 1)
    {
        CurrentBp = Math.Min(CurrentBp + amount, 5);
    }
    
    public void Defend()
    {
        IsDefendingThisRound = true;
        HasDefendPriorityNextRound = true;
    }

    public void GrantTurnPriority()
    {
        HasTurnPriorityFromSkill = true;
    }

    public void ApplyTurnDelay(int rounds)
    {
        RoundsInLastTurn += rounds;
    }

    public void MarkAsRevived()
    {
        RevivedThisRound = true;
    }

    public void TickTurnDelay()
    {
        if (RoundsInLastTurn > 0) RoundsInLastTurn--;
    }
    
    protected void InitializeCurrentStats()
    {
        CurrentHp = BaseStats.MaxHp;
        CurrentSp = BaseStats.MaxSp;
    }

    protected void SetInitialBp(int bp)
    {
        CurrentBp = bp;
    }
    public void IncreaseMaxHp(int bonus)
    {
        BaseStats.MaxHp += bonus;
        CurrentHp += bonus;
    }
    
    public virtual string GetStatsSummary()
        => $"{Name} - HP:{CurrentHp}/{BaseStats.MaxHp} SP:{CurrentSp}/{BaseStats.MaxSp} BP:{CurrentBp}";

    public virtual TurnPriorityCategory GetCategoryForCurrentRound()
    {
        if (HasDefendPriorityThisRound) return TurnPriorityCategory.DefendedLastRound;
        if (HasTurnPriorityThisRound) return TurnPriorityCategory.PrioritizedBySkill;
        return TurnPriorityCategory.Normal;
    }

    public virtual TurnPriorityCategory GetCategoryForNextRound()
    {
        if (HasDefendPriorityNextRound) return TurnPriorityCategory.DefendedLastRound;
        if (HasTurnPriorityFromSkill) return TurnPriorityCategory.PrioritizedBySkill;
        return TurnPriorityCategory.Normal;
    }
    // cambios E3
    public void ApplyStatEffect(StatModifierType type, int rounds)
    {
        var existing = _statusEffects.FirstOrDefault(e => e.Type == type);
        if (existing != null) existing.ExtendDuration(rounds);
        else _statusEffects.Add(new StatEffect(type, rounds));
    }

    public bool HasStatEffect(StatModifierType type)
        => _statusEffects.Any(e => e.Type == type);

    public void TickStatEffects()
    {
        foreach (var effect in _statusEffects) effect.Tick();
        _statusEffects.RemoveAll(e => e.HasExpired);
    }
    public double EffectiveSpeed
    {
        get
        {
            double speed = BaseStats.Speed;
            if (HasStatEffect(StatModifierType.IncreasedSpeed))
                speed *= SpeedIncreasedFactor;
            if (HasStatEffect(StatModifierType.DecreasedSpeed))
                speed *= SpeedDecreasedFactor;
            return speed;
        }
    }
    public double EffectiveSpeedAfterTick
    {
        get
        {
            double speed = BaseStats.Speed;
            bool hasIncreasedAfterTick = _statusEffects
                .Any(e => e.Type == StatModifierType.IncreasedSpeed && e.RemainingRounds > 1);
            bool hasDecreasedAfterTick = _statusEffects
                .Any(e => e.Type == StatModifierType.DecreasedSpeed && e.RemainingRounds > 1);
            if (hasIncreasedAfterTick) speed *= SpeedIncreasedFactor;
            if (hasDecreasedAfterTick) speed *= SpeedDecreasedFactor;
            return speed;
        }
    }
}