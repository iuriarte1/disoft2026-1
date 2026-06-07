using Octopath_Traveler_Model;
using Octopath_Traveler.ActiveSkills.BeastSelectionSkill;
using Octopath_Traveler.ActiveSkills.DivineSkills;

namespace Octopath_Traveler.ActiveSkills;

public class SkillEffectFactory
{
    public static IActiveSkillEffect Create(Skill skill, Traveler actor, Beast victim, Traveler ally, List<Traveler> playerTeam, int teamBpBeforeSpend = 0)
    {
        return skill.Name switch
        {
            "Vivify" => new VivifySkillEffect(skill, ally),
            "Revive" => new RevivePartySkillEffect(skill),
            "Spearhead" => new SpearHeadSkillEffect(skill, victim, actor),
            "Leghold Trap" => new LegholdTrapSkillEffect(victim),
            "First Aid" => new AllyHealingSkillEffect(skill, ally),
            "Heal Wounds" => new PartyHealingSkillEffect(skill, actor),
            "Heal More" => new PartyHealingSkillEffect(skill, actor),
            "Last Stand" => new LastStandSkillEffect(actor, skill),
            "Mercy Strike" => new MercyStrikeSkillEffect(skill, victim),
            "Shooting Stars" => new ShootingStarsSkillEffect(skill),
            "Nightmare Chimera" => new NightmareChimeraSkillEffect(skill, victim, actor),
            "Thousand Spears"  => new AutoSelectSingleTargetSkill(skill, living => living.MinBy(b => b.BaseStats.PhysicalDefense)),
            "Rain of Arrows"   => new AutoSelectSingleTargetSkill(skill, living => living.MinBy(b => b.CurrentHp)),
            "Guardian Liondog" => new AutoSelectSingleTargetSkill(skill, living => living.MaxBy(b => b.BaseStats.Speed)),
            "HP Thief" => new HpThiefSkillEffect(skill, victim),
            "Steal SP"  => new StealSpSkillEffect(skill, victim),

            // Buff y debuff
            "Lion Dance"      => new AllyBuffSkillEffect(skill, ally,new[] { StatModifierType.IncreasedPhysicalAttack }, 2),
            "Peacock Strut"   => new AllyBuffSkillEffect(skill, ally,new[] { StatModifierType.IncreasedElementalAttack }, 2),
            "Stout Wall"      => new UserBuffSkillEffect(skill,new[] { StatModifierType.IncreasedPhysicalDefense }, 3),
            "Shackle Foe"     => new SingleDebuffSkillEffect(skill, victim, new[] { StatModifierType.DecreasedPhysicalAttack }, 2),
            "Armor Corrosive" => new SingleDebuffSkillEffect(skill, victim, new[] { StatModifierType.DecreasedPhysicalDefense }, 2),
            "Starsong"        => new AllyBuffSkillEffect(skill, ally, new[] { StatModifierType.IncreasedPhysicalDefense, StatModifierType.IncreasedElementalDefense, StatModifierType.IncreasedSpeed }, 2),
            "Sheltering Veil" => new AllyBuffSkillEffect(skill, ally, new[] { StatModifierType.IncreasedElementalDefense }, 2),
            "Abide" => new AllyBuffSkillEffect(skill, ally, new[] { StatModifierType.IncreasedPhysicalAttack }, 2),
            "Mole Dance" => new AllyBuffSkillEffect(skill, ally, new[] { StatModifierType.IncreasedPhysicalDefense }, 2),
            "Panther Dance" => new AllyBuffSkillEffect(skill, ally, new[] { StatModifierType.IncreasedSpeed }, 2),
            //hibridas
            "Elemental Break" => new ElementalBreakSkillEffect(skill, victim, StatModifierType.DecreasedElementalDefense, 2),
            //Divinas
            "Winnehild's Battle Cry" => new WinnehildsBattleCrySkillEffect(skill),
            "Balogar's Blade"        => new BalogarsBladSkillEffect(skill, victim),
            "Steorra's Prophecy" => new SteorrasProphecySkillEffect(skill, teamBpBeforeSpend - 3),
            _ when skill.Target == "Single"   => new SingleTargetOffensiveSkill(skill, victim),
            _ when skill.Target == "Enemies"  => new AllEnemiesOffensiveSkill(skill)
        };
    }
}