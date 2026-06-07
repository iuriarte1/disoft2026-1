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
            "Fire Storm" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 2)),
            "Blizzard" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 2)),
            "Lightning Blast" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 2)),
            "Ignis Ardere" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 3)),
            "Glacies Claudere" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 3)),
            "Tonitrus Canere" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 3)),
            "Ventus Saltare" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 3)),
            "Lux Congerere" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 3)),
            "Tenebrae Operire" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 3)),
            "Arrowstorm" => new AllEnemiesOffensiveSkill(Skill.WithHits(skill, 6)),
            "Thousand Spears" => new AutoSelectSingleTargetSkill(Skill.WithHits(skill, 7), living => living.MinBy(b => b.BaseStats.PhysicalDefense)),
            "Rain of Arrows" => new AutoSelectSingleTargetSkill(Skill.WithHits(skill, 6), living => living.MinBy(b => b.CurrentHp)),
            "Guardian Liondog" => new AutoSelectSingleTargetSkill(Skill.WithHits(skill, 5), living => living.MaxBy(b => b.BaseStats.Speed)),
            "HP Thief" => new HpThiefSkillEffect(Skill.WithHits(skill, 2), victim),
            "Steal SP"  => new StealSpSkillEffect(Skill.WithHits(skill, 2), victim),

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