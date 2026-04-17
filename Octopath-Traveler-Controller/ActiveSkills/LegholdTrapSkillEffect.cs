using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class LegholdTrapSkillEffect :IActiveSkillEffect
{
    private readonly Beast _victim;
    private const int RoundsInLastPlace = 2;
    private readonly string _name = "Leghold Trap";

    public LegholdTrapSkillEffect(Beast victim)
    {
        _victim = victim;
    }

    public void Execute(Traveler atacante, List<Traveler> playerTeam,List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(atacante.Name, _name);
        _victim.RoundsInLastTurn = RoundsInLastPlace;
        view.ShowLegholdTrapEffect(_victim.Name, RoundsInLastPlace);
    }


}