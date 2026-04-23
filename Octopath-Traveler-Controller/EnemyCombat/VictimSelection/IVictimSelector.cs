using Octopath_Traveler_Model;

namespace Octopath_Traveler.EnemyCombat.VictimSelection;

public interface IVictimSelector
{
    Traveler SelectVictim(List<Traveler> playerTeam);
}