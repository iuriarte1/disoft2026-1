using Octopath_Traveler_Model;
using Octopath_Traveler.ActiveSkills.BeastSelectionSkill;
using Octopath_Traveler.ActiveSkills.DivineSkills;

namespace Octopath_Traveler.ActiveSkills;

public class SkillEffectFactory
{
    public static IActiveSkillEffect Create(Skill skill, Traveler actor, Beast victim, Traveler ally,
        List<Traveler> playerTeam, int teamBpBeforeSpend = 0, int bpUsed = 0)
    {
        Skill effectiveSkill = ApplyBoostToModifier(skill, bpUsed);
        return effectiveSkill.Name switch
        {
            "Vivify" => new VivifySkillEffect(effectiveSkill, ally),
            "Revive" => new RevivePartySkillEffect(effectiveSkill, bpUsed),
            "Spearhead" => new SpearHeadSkillEffect(effectiveSkill, victim, actor),
            "Leghold Trap" => new LegholdTrapSkillEffect(victim, bpUsed),
            "First Aid" => new AllyHealingSkillEffect(effectiveSkill, ally),
            "Heal Wounds" => new PartyHealingSkillEffect(Skill.WithBoostedModifier(skill, skill.Modifier + 0.5 * bpUsed), actor),
            "Heal More" => new PartyHealingSkillEffect(effectiveSkill, actor),
            "Last Stand" => new LastStandSkillEffect(actor, effectiveSkill),
            "Mercy Strike" => new MercyStrikeSkillEffect(effectiveSkill, victim),
            "Shooting Stars" => new ShootingStarsSkillEffect(effectiveSkill),
            "Nightmare Chimera" => new NightmareChimeraSkillEffect(effectiveSkill, victim, actor),
            "Fire Storm" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 2)),
            "Blizzard" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 2)),
            "Lightning Blast" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 2)),
            "Ignis Ardere" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 3)),
            "Glacies Claudere" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 3)),
            "Tonitrus Canere" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 3)),
            "Ventus Saltare" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 3)),
            "Lux Congerere" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 3)),
            "Tenebrae Operire" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 3)),
            "Arrowstorm" => new AllEnemiesOffensiveSkill(Skill.WithHits(effectiveSkill, 6)),
            "Thousand Spears" => new AutoSelectSingleTargetSkill(Skill.WithHits(effectiveSkill, 7), living => living.MinBy(b => b.BaseStats.PhysicalDefense)),
            "Rain of Arrows" => new AutoSelectSingleTargetSkill(Skill.WithHits(effectiveSkill, 6), living => living.MinBy(b => b.CurrentHp)),
            "Guardian Liondog" => new AutoSelectSingleTargetSkill(Skill.WithHits(effectiveSkill, 5), living => living.MaxBy(b => b.BaseStats.Speed)),
            "HP Thief" => new HpThiefSkillEffect(Skill.WithHits(effectiveSkill, 2), victim),
            "Steal SP"  => new StealSpSkillEffect(Skill.WithHits(effectiveSkill, 2), victim),
            // Buff y debuff
            "Lion Dance"      => new AllyBuffSkillEffect(effectiveSkill, ally,new[] { StatModifierType.IncreasedPhysicalAttack }, 2, bpUsed, 2),
            "Peacock Strut"   => new AllyBuffSkillEffect(effectiveSkill, ally,new[] { StatModifierType.IncreasedElementalAttack }, 2, bpUsed, 2),
            "Stout Wall"      => new UserBuffSkillEffect(effectiveSkill,new[] { StatModifierType.IncreasedPhysicalDefense }, 3, bpUsed, 2),
            "Shackle Foe"     => new SingleDebuffSkillEffect(effectiveSkill, victim, new[] { StatModifierType.DecreasedPhysicalAttack }, 2, bpUsed, 2),
            "Armor Corrosive" => new SingleDebuffSkillEffect(effectiveSkill, victim, new[] { StatModifierType.DecreasedPhysicalDefense }, 2, bpUsed, 2),
            "Starsong"        => new AllyBuffSkillEffect(effectiveSkill, ally, new[] { StatModifierType.IncreasedPhysicalDefense, StatModifierType.IncreasedElementalDefense, StatModifierType.IncreasedSpeed }, 2, bpUsed, 2),
            "Sheltering Veil" => new AllyBuffSkillEffect(effectiveSkill, ally, new[] { StatModifierType.IncreasedElementalDefense }, 2, bpUsed, 2),
            "Abide" => new AllyBuffSkillEffect(effectiveSkill, ally, new[] { StatModifierType.IncreasedPhysicalAttack }, 2, bpUsed, 2),
            "Mole Dance" => new AllyBuffSkillEffect(effectiveSkill, ally, new[] { StatModifierType.IncreasedPhysicalDefense }, 2, bpUsed, 2),
            "Panther Dance" => new AllyBuffSkillEffect(effectiveSkill, ally, new[] { StatModifierType.IncreasedSpeed }, 2, bpUsed, 2),
            //hibridas
            "Elemental Break" => new ElementalBreakSkillEffect(effectiveSkill, victim, StatModifierType.DecreasedElementalDefense, 2),
            //Divinas
            "Winnehild's Battle Cry" => new WinnehildsBattleCrySkillEffect(effectiveSkill),
            "Balogar's Blade"        => new BalogarsBladSkillEffect(effectiveSkill, victim),
            "Steorra's Prophecy" => new SteorrasProphecySkillEffect(effectiveSkill, teamBpBeforeSpend - 3),
            _ when skill.Target == "Single"   => new SingleTargetOffensiveSkill(effectiveSkill, victim),
            _ when skill.Target == "Enemies"  => new AllEnemiesOffensiveSkill(effectiveSkill)
        };
    }
    private static Skill ApplyBoostToModifier(Skill skill, int bpUsed)
    {
        if (bpUsed == 0 || skill.IsDivine || skill.Modifier == 0) return skill;

        double boostPercent = SkillBoostParser.ParseModifierPercent(skill.Boost);
        if (boostPercent == 0) return skill;

        return Skill.WithBoostedModifier(skill, skill.Modifier * (1 + boostPercent * bpUsed));
    }
}