using Octopath_Traveler_Model;
using Octopath_Traveler.EnemyCombat.VictimSelection;

namespace Octopath_Traveler.EnemyCombat.BeastSkill;

public class BeastSkillFactory
{
    public IBeastSkillEffect Create(Skill skill)
    {

        return skill.Name switch
        {
            "Attack" => new BeastSingleTargetSkill(skill, new TravelerWithHighestHp()),
            "Befuddling claw" => new BeastSingleTargetSkill(skill, new TravelerWithHighestElemAtk()),
            "Stab"            => new BeastSingleTargetSkill(skill, new TravelerWithLowestPhysDef()),
            "Boar Rush"       => new BeastSingleTargetSkill(skill, new TravelerWithLowestPhysDef()),
            "Vorpal Fang"     => new BeastSingleTargetSkill(skill, new TravelerWithLowestPhysDef()),
            "Meteor Storm"    => new BeastSingleTargetSkill(skill, new TravelerWithHighestSpeed()),
            "Freeze"          => new BeastSingleTargetSkill(skill, new TravelerWithHighestSpeed()),
            "Luminescence"    => new BeastSingleTargetSkill(skill, new TravelerWithHighestSpeed()),
            "Enshadow"        => new BeastSingleTargetSkill(skill, new TravelerWithHighestSpeed()),
            "Wind Slash"      => new BeastSingleTargetSkill(skill, new TravelerWithHighestSpeed()),
            "Windshot"        => new BeastSingleTargetSkill(skill, new TravelerWithLowestElemDef()),
            "Firesand"        => new BeastSingleTargetSkill(skill, new TravelerWithLowestElemDef()),
            "Thundershot"     => new BeastSingleTargetSkill(skill, new TravelerWithLowestElemDef()),
            "Lightshot"       => new BeastSingleTargetSkill(skill, new TravelerWithLowestElemDef()),
            "Iceshot"         => new BeastSingleTargetSkill(skill, new TravelerWithLowestElemDef()),
            "Shadowshot"      => new BeastSingleTargetSkill(skill, new TravelerWithLowestElemDef()),
            "Stampede"        => new BeastAllEnemiesSkill(skill),
            "Rampage"         => new BeastAllEnemiesSkill(skill),
            "Ice Blast"       => new BeastAllEnemiesSkill(skill),
            "Incinerate"      => new BeastAllEnemiesSkill(skill),
            "Black Gale"      => new BeastAllEnemiesSkill(skill),
            "Galestorm"       => new BeastAllEnemiesSkill(skill),
            "Vortal Claw"     => new VortalClawSkillEffect(skill)
        };
    }
}