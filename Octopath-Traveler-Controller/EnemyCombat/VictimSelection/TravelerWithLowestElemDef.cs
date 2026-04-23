using Octopath_Traveler_Model;

namespace Octopath_Traveler.EnemyCombat.VictimSelection;

public class TravelerWithLowestElemDef : IVictimSelector
{
    public Traveler SelectVictim(List<Traveler> travelers) 
        => travelers.OrderBy(t => t.BaseStats.ElementalDefense).First();
}