using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills;

public class ShootingStarsSkillEffect : IActiveSkillEffect
{
    private Skill _skill;
    private string[] _types = { "Wind", "Light", "Dark" };

    public ShootingStarsSkillEffect(Skill skill)
    {
        _skill = skill;
    }
    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        view.ShowSkillUsed(actor.Name, _skill.Name);
        var skills = CreateOneSkillForEachType();
        foreach (Beast victim in enemyTeam)
        {
            foreach (var skill in skills)
            {
                var effect = new SingleTargetOffensiveSkill(skill, victim, false, false);
                effect.Execute(actor, playerTeam, enemyTeam, view);
            }
        }
        ShowOneTimeFinalHp(enemyTeam, view);
    }

    private void ShowOneTimeFinalHp(List<Beast> enemyTeam, View view)
    {
        foreach (var enemy in enemyTeam)
            view.ShowFinalHp(enemy.Name, enemy.CurrentHp);
    }

    private List<Skill> CreateOneSkillForEachType()
    {
        List<Skill> skills = new List<Skill>();
        foreach (var type in _types)
        {
            Skill skill = new Skill{ 
                Name = _skill.Name,
                SP = _skill.SP,
                Type = type,
                Modifier = _skill.Modifier,
                Target = _skill.Target
            };
            skills.Add(skill);
        }
        return skills;
    }
}