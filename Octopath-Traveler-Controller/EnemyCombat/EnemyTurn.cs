using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.EnemyCombat.BeastSkill;
using Octopath_Traveler.PassiveSkills;

namespace Octopath_Traveler.EnemyCombat;

public class EnemyTurn
{
    private readonly Beast _actor;
    private readonly View _view;
    private readonly List<Traveler> _playerTeam;
    private readonly List<Beast> _enemyTeam;
    private readonly PassiveSkillManager _passiveManager;

    public EnemyTurn(Beast actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view, PassiveSkillManager passiveManager)
    {
        _actor = actor;
        _view = view;
        _playerTeam = playerTeam;
        _enemyTeam = enemyTeam;
        _passiveManager = passiveManager;
    }

    public void Execute()
    {
        new BeastSkillFactory(_enemyTeam, _passiveManager).Create(_actor.Skill).Execute(_actor, _playerTeam, _view);
    }
}