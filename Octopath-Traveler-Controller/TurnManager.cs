using Octopath_Traveler_Model;

namespace Octopath_Traveler;

public class TurnManager
{
    private readonly List<Unit> _allCombatants;

    public TurnManager(List<Traveler> players, List<Beast> enemies)
    {
        // Guardamos a todos los participantes en una sola gran lista desde el principio
        _allCombatants = players.Cast<Unit>().Concat(enemies.Cast<Unit>()).ToList();
    }

    // Calcula los turnos de la ronda actual
    public List<Unit> GetCurrentRoundTurns()
    {
        return _allCombatants
            .Where(u => !u.IsDead) // Solo los vivos entran a la fila
            .OrderByDescending(u => u.BaseStats.Speed)
            .ToList();
    }

    // Calcula los turnos de la próxima ronda (Vital para la interfaz de Octopath)
    public List<Unit> GetNextRoundTurns()
    {
        // En la Entrega 1, la próxima ronda suele ser igual a la actual, 
        // pero al tener esto separado, en el futuro podrás programar la mecánica de "Break" aquí.
        return _allCombatants
            .Where(u => !u.IsDead)
            .OrderByDescending(u => u.BaseStats.Speed) 
            .ToList();
    }

    // Método de apoyo para convertir las unidades en los textos que tu View necesita
    public List<string> GetTurnNames(List<Unit> turnList)
    {
        return turnList.Select(u => u.Name).ToList();
    }
}