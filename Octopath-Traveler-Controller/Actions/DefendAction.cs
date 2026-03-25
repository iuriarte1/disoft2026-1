using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Actions;

public class DefendAction : ICombatAction
{
    public void Execute(Traveler actor, List<Traveler> playerTeam, List<Beast> enemyTeam, View view)
    {
        // En Octopath, defender suele hacer que actúes primero en el siguiente turno 
        // o reducir el daño recibido. Puedes crear un estado "IsDefending" en Unit.cs
        view.WriteLine($"{actor.Name} adopta una postura defensiva.");
    }
}