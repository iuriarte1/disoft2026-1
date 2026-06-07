using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.ActiveSkills.DivineSkills;

public class WinnehildsBattleCrySkillEffect : IActiveSkillEffect
{
    private static readonly string[] WeaponTypes = 
        { "Sword", "Spear", "Dagger", "Axe", "Bow", "Stave" };
    private readonly Skill _skill;
    private List<Beast> _enemyTeam;

    public WinnehildsBattleCrySkillEffect(Skill skill)
    {
        _skill = skill;
    }

    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        _enemyTeam = enemyTeam.Where(b => !b.IsDead).ToList();
        view.ShowSkillUsed(actor.Name, _skill.Name);
        foreach (var enemy in _enemyTeam)
            ApplyAllWeaponsToEnemy(actor, enemy, view);
        ShowFinalHpOfAll(_enemyTeam, view);
    }

    private void ApplyAllWeaponsToEnemy(Traveler actor, Beast enemy, View view)
    {
        foreach (var weaponType in WeaponTypes)
        {
            var fakeSkill = new Skill 
            { 
                Name = _skill.Name, Type = weaponType, 
                Modifier = _skill.Modifier, Target = "Enemies" 
            };
            var (damage, enteredBreakingPoint) = ActiveSkillDamageCalculator.Calculate(actor, enemy, fakeSkill);
            enemy.TakeDamage(damage);
            ShowHitMessage(enemy, weaponType, damage, view);
            if (enteredBreakingPoint) view.ShowBreakingPoint(enemy.Name);
        }
    }

    private void ShowHitMessage(Beast enemy, string weaponType, int damage, View view)
    {
        bool isWeakness = enemy.Weaknesses.Contains(weaponType);
        if (isWeakness)
            view.ShowSkillDamageResultWithWeakness(enemy.Name, weaponType, damage);
        else
            view.ShowSkillDamageResult(enemy.Name, weaponType, damage);
    }

    private void ShowFinalHpOfAll(List<Beast> enemies, View view)
    {
        foreach (var enemy in enemies)
            view.ShowFinalHp(enemy.Name, enemy.CurrentHp);
    }
}