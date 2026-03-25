using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class UseSkillAction : ICombatAction
{
    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        // 1. Mostrar habilidades activas del traveler
        // 2. Pedir al jugador que elija una
        // 3. Verificar si tiene SP suficiente
        // 4. Pedir objetivo (enemigo o aliado dependiendo de la habilidad)
        // 5. Aplicar efecto y restar SP
        view.WriteLine($"{actor.Name} se prepara para usar una habilidad... (Lógica en construcción)");
    }
}
