using Octopath_Traveler_Model;

namespace Octopath_Traveler.ActiveSkills;

public class SkillEffectFactory
{
    public static IActiveSkillEffect Create(Skill skill, Traveler actor, Beast victim, Traveler ally)
    {
        return skill.Name switch
        {
            "Vivify"       => new VivifySkillEffect(skill, ally),
            "Revive"       => new RevivePartySkillEffect(skill),
            "Spearhead"    => new SpearHeadSkillEffect(skill, victim, actor),
            "Leghold Trap" => new LegholdTrapSkillEffect(victim),
            "First Aid"    => new AllyHealingSkillEffect(skill, ally),
            "Heal Wounds"  => new PartyHealingSkillEffect(skill, actor),
            "Heal More"    => new PartyHealingSkillEffect(skill, actor),
            "Last Stand" => new LastStandSkillEffect(actor, skill),
            "Mercy Strike" => new MercyStrikeSkillEffect(skill, victim),
            "Shooting Stars" => new ShootingStarsSkillEffect(skill),
            "Nightmare Chimera" => new NightmareChimeraSkillEffect(skill, victim, actor),
            _ when skill.Target == "Single"   => new SingleTargetOffensiveSkill(skill, victim),
            _ when skill.Target == "Enemies"  => new AllEnemiesOffensiveSkill(skill)
        };
    }
}