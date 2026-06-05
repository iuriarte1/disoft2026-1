using OctopathTravelerGUI.Models.Enums;
using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler.ClassesGUI;

/*
 * Esta es una clase auxiliar. Ocurre que los métodos de la vista GUI reciben objetos
 * que implementen la interfaz IState. Por eso, para crear el ejemplo, era necesario
 * crear un objeto que implemente esa interfaz.
 */
public class StateGUI(IEnumerable<ITraveler> travelers, IEnumerable<IBeast> beasts, IEnumerable<string> options, 
    Option option, IEnumerable<string> currentRoundTurns, IEnumerable<string> nextRoundTurns)
    : IState
{
    public IEnumerable<ITraveler> Travelers { get; set; } = travelers;
    public IEnumerable<IBeast> Beasts { get; set; } = beasts;
    public IEnumerable<string> Options { get; set; } = options;
    public Option Option { get; set; } = option;
    public IEnumerable<string> CurrentRoundTurns { get; set; } = currentRoundTurns;
    public IEnumerable<string> NextRoundTurns { get; set; } = nextRoundTurns;
}