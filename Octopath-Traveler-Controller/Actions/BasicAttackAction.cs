using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class BasicAttackAction : ICombatAction
{
    private string _weaponChoosen;
    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        // 1. Mostrar a los enemigos vivos para que el jugador elija a quién atacar
        var livingEnemies = enemyTeam.Where(e => !e.IsDead).ToList();
        
        // (Aquí llamarías a un método de la View para que el jugador elija el índice del enemigo)
        // Por ahora, simulamos que ataca al primero para que no te explote el código:
        Beast target = livingEnemies.First(); 

        // 2. Calcular daño (Ataque físico vs Defensa física)
        int damage = Math.Max(1, actor.BaseStats.PhysicalAttack - target.BaseStats.PhysicalDefense);
        
        // 3. Aplicar daño
        target.TakeDamage(damage);

        // 4. Mostrar resultado
        view.WriteLine($"{actor.Name} ataca a {target.Name} con su arma y causa {damage} de daño.");
        if (target.IsDead) view.WriteLine($"{target.Name} ha sido derrotado.");
    }

    private void ChooseWeaponToAttack(Traveler actor, View view)
    {
        int option = view.GetWeaponOption(actor.Weapons);
        if (option > actor.Weapons.Count)
        {
            // ataque cancelado hya que volver a mostrar las opciones de ataque
            return;
        }
        _weaponChoosen = actor.Weapons[option - 1];
    }
    private void ChooseVictimToAttack(List<Beast> enemyTeam, View view)
    {
        // Aquí mostrarías a los enemigos vivos y pedirías al jugador que elija uno
        // Por ahora, simulamos que ataca al primero para que no te explote el código:
        Beast target = enemyTeam.First(e => !e.IsDead);
    }
    private void DamageCalculatorFromWeapon()
}