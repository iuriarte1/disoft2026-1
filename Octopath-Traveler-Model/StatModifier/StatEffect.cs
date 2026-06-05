namespace Octopath_Traveler_Model;

public class StatEffect
{
    public StatModifierType Type { get; }
    public int RemainingRounds { get; private set; }

    public StatEffect(StatModifierType type, int rounds)
    {
        Type = type;
        RemainingRounds = rounds;
    }

    public void ExtendDuration(int extraRounds) => RemainingRounds += extraRounds;
    public void Tick() => RemainingRounds--;
    public bool HasExpired => RemainingRounds <= 0;
}