using Octopath_Traveler_Model;

namespace Octopath_Traveler.EnemyCombat.VictimSelection;

public class TravelerWithHighestPhysDef : IVictimSelector
{
    public Traveler SelectVictim(List<Traveler> travelers)
        => travelers.MaxBy(t => t.BaseStats.PhysicalDefense);
}