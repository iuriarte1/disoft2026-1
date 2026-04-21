using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.ActiveSkills;

public class NightmareChimeraSkillEffect : IActiveSkillEffect
{
    private Beast _victim;
    private Traveler _actor;
    private Skill _skill;

    public NightmareChimeraSkillEffect(Skill skill, Beast vicitm, Traveler actor)
    {
        _victim = vicitm;
        _actor = actor;
        _skill = skill;
    }

    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        string weapon = new WeaponOptionManager(view, actor, true).GetWeaponChoosen();
        if (weapon == null) return;
        Beast victim = new VictimOptionManager(view, enemyTeam.Where(e => !e.IsDead).ToList(), actor.Name).GetVictimChoosen();
        if (victim == null) return;
        int bp = view.GetHowManyBoostPointsToUse();
        var skillModified = SkillTypeModifiedWithWeapon(weapon);
        var effect = new SingleTargetOffensiveSkill(skillModified, victim);
        effect.Execute(actor, playerTeam, enemyTeam, view);
    }
    private Skill SkillTypeModifiedWithWeapon(string weapon)
    {
        return new Skill
        {
            Name = _skill.Name,
            SP = _skill.SP,
            Type = weapon,
            Modifier = _skill.Modifier,
            Target = _skill.Target
        };
    }
}