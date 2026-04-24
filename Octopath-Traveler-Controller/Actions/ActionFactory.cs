namespace Octopath_Traveler.Actions;

public static class ActionFactory
{
    public static ICombatAction Create(string choice) => choice switch
    {
        "1" => new BasicAttackAction(),
        "2" => new UseSkillAction(),
        "3" => new DefendAction(),
        "4" => new RunAwayAction(),
        _   => new BasicAttackAction()
    };
}