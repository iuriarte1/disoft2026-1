using Octopath_Traveler_Model;

namespace Octopath_Traveler.EnemyCombat.VictimSelection;

public class TravelerWithHighestSpeed : IVictimSelector
{
    public Traveler SelectVictim(List<Traveler> travelers) 
        => travelers.OrderByDescending(t => t.BaseStats.Speed).First();
}