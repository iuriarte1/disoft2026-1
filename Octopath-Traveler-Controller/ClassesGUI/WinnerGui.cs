using OctopathTravelerGUI.Models.Enums;
using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler;

/*
 * Esta es una clase auxiliar. Ocurre que los métodos de la vista GUI reciben objetos
 * que implementen la interfaz IWinner. Por eso, para crear el ejemplo, era necesario
 * crear un objeto que implemente esa interfaz.
 */
public class WinnerGui(WinnerOption winnerOption, IEnumerable<string> team)
    : IWinner
{
    public WinnerOption WinnerOption { get; set; } = winnerOption;
    public IEnumerable<string> Team { get; set; } = team;
}