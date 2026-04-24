using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.BeastSkill;

namespace Octopath_Traveler.EnemyCombat;

public class EnemyTurn
{
    private readonly Beast _actor;
    private readonly View _view;
    private readonly List<Traveler> _playerTeam;
    public EnemyTurn(Beast actor,List<Traveler> team, View view)
    {
        _actor = actor;
        _view = view;
        _playerTeam = team;
    }
    public void Execute()
    {
        new BeastSkillFactory().Create(_actor.Skill).Execute(_actor, _playerTeam, _view);
    }
   
}