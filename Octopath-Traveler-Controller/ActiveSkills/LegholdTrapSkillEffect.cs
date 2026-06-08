using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class LegholdTrapSkillEffect : IActiveSkillEffect
{
    private readonly Beast _victim;
    private readonly int _totalRounds;
    private const int BaseRounds = 2;
    private const int RoundsPerBp = 2;
    private readonly string _name = "Leghold Trap";

    public LegholdTrapSkillEffect(Beast victim, int bpUsed)
    {
        _victim = victim;
        _totalRounds = BaseRounds + RoundsPerBp * bpUsed;
    }

    public void Execute(Traveler atacante, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(atacante.Name, _name);
        _victim.ApplyTurnDelay(_totalRounds);
        view.ShowLegholdTrapEffect(_victim.Name, _totalRounds);
    }
}