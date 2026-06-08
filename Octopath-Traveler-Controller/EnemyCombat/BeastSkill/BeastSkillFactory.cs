using Octopath_Traveler_Model;
using Octopath_Traveler.EnemyCombat.VictimSelection;
using Octopath_Traveler.PassiveSkills;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastSkillFactory
{
    private readonly List<Beast> _enemyTeam;
    private readonly PassiveSkillManager _passiveManager;

    public BeastSkillFactory(List<Beast> enemyTeam, PassiveSkillManager passiveManager)
    {
        _enemyTeam = enemyTeam;
        _passiveManager = passiveManager;
    }

    public IBeastSkillEffect Create(Skill skill) => skill.Name switch
    {
        "Attack" => new BeastSingleTargetSkill(skill, "Phys", new TravelerWithHighestHp(), _passiveManager),
        "Befuddling claw" => new BeastSingleTargetSkill(skill, "Phys", new TravelerWithHighestElemAtk(), _passiveManager),
        "Stab" => new BeastSingleTargetSkill(skill, "Phys", new TravelerWithLowestPhysDef(), _passiveManager),
        "Boar Rush" => new BeastSingleTargetSkill(skill, "Phys", new TravelerWithLowestPhysDef(), _passiveManager),
        "Vorpal Fang" => new BeastSingleTargetSkill(skill, "Phys", new TravelerWithLowestPhysDef(), _passiveManager),
        "Meteor Storm" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithHighestSpeed(), _passiveManager),
        "Freeze" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithHighestSpeed(), _passiveManager),
        "Luminescence" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithHighestSpeed(), _passiveManager),
        "Enshadow" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithHighestSpeed(), _passiveManager),
        "Wind slash" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithHighestSpeed(), _passiveManager),
        "Windshot" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithLowestElemDef(), _passiveManager),
        "Firesand" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithLowestElemDef(), _passiveManager),
        "Thundershot" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithLowestElemDef(), _passiveManager),
        "Lightshot" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithLowestElemDef(), _passiveManager),
        "Iceshot" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithLowestElemDef(), _passiveManager),
        "Shadowshot" => new BeastSingleTargetSkill(skill, "Elem", new TravelerWithLowestElemDef(), _passiveManager),
        "Stampede" => new BeastAllEnemiesSkill(skill, "Phys", _passiveManager),
        "Rampage" => new BeastAllEnemiesSkill(skill, "Phys", _passiveManager),
        "Ice blast" => new BeastAllEnemiesSkill(skill, "Elem", _passiveManager),
        "Incinerate" => new BeastAllEnemiesSkill(skill, "Elem", _passiveManager),
        "Black Gale" => new BeastAllEnemiesSkill(skill, "Elem", _passiveManager),
        "Galestorm" => new BeastAllEnemiesSkill(skill, "Elem", _passiveManager),
        "Vortal Claw" => new VortalClawSkillEffect(skill),
        "Double Bite" => new BeastSingleTargetSkill(Skill.WithHits(skill, 2), "Phys", new TravelerWithLowestPhysDef(), _passiveManager),
        "Shadow Magic" => new BeastAllEnemiesSkill(Skill.WithHits(skill, 2), "Elem", _passiveManager),
        "Triple Slash" => new BeastSingleTargetSkill(Skill.WithHits(skill, 3), "Phys", new TravelerWithHighestHp(), _passiveManager),
        "Consume Armor" => new ConsumeArmourSkillEffect(skill),
        "Flap" => new FlapSkillEffect(skill),
        "Acid Spray" => new AcidSpraySkillEffect(skill),
        "Gather Strength" => new GatherStrengthSkillEffect(skill),
        "Augmented Magic" => new AugmentedMagicSkillEffect(skill, _enemyTeam),
        "Volcano" => new VolcanoSkillEffect(skill)
    };
}