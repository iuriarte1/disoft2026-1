using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class RunAwayAction : ICombatAction
{
public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
{
    // Aquí puedes poner una probabilidad matemática de éxito.
    view.WriteLine($"{actor.Name} intenta huir del combate...");
    // Si tiene éxito, puedes poner la vida de los enemigos a 0 para forzar el fin del combate, 
    // o crear una variable "BattleFled" en tu CombatManager.
}
}