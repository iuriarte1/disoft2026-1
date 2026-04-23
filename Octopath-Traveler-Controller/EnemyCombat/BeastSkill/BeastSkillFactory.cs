using Octopath_Traveler_Model;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastSkillFactory
{
    public IBeastSkillEffect Create(Skill skill)
    {

        return skill.Name switch
        {
            "Attack" => new BeastSingleTargetSkill(skill,"Phys", new TravelerWithHighestHp()),
            "Befuddling claw" => new BeastSingleTargetSkill(skill,"Phys", new TravelerWithHighestElemAtk()),
            "Stab"            => new BeastSingleTargetSkill(skill,"Phys", new TravelerWithLowestPhysDef()),
            "Boar Rush"       => new BeastSingleTargetSkill(skill,"Phys", new TravelerWithLowestPhysDef()),
            "Vorpal Fang"     => new BeastSingleTargetSkill(skill,"Phys", new TravelerWithLowestPhysDef()),
            "Meteor Storm"    => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithHighestSpeed()),
            "Freeze"          => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithHighestSpeed()),
            "Luminescence"    => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithHighestSpeed()),
            "Enshadow"        => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithHighestSpeed()),
            "Wind slash"      => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithHighestSpeed()),
            "Windshot"        => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithLowestElemDef()),
            "Firesand"        => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithLowestElemDef()),
            "Thundershot"     => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithLowestElemDef()),
            "Lightshot"       => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithLowestElemDef()),
            "Iceshot"         => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithLowestElemDef()),
            "Shadowshot"      => new BeastSingleTargetSkill(skill,"Elem", new TravelerWithLowestElemDef()),
            "Stampede"        => new BeastAllEnemiesSkill(skill,"Phys"),
            "Rampage"         => new BeastAllEnemiesSkill(skill,"Phys"),
            "Ice blast"       => new BeastAllEnemiesSkill(skill,"Elem"),
            "Incinerate"      => new BeastAllEnemiesSkill(skill,"Elem"),
            "Black Gale"      => new BeastAllEnemiesSkill(skill,"Elem"),
            "Galestorm"       => new BeastAllEnemiesSkill(skill,"Elem"),
            "Vortal Claw"     => new VortalClawSkillEffect(skill)
        };
    }
}