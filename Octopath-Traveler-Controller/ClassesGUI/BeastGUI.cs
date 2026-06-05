using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler.ClassesGUI;

/*
 * Esta es una clase auxiliar. Ocurre que los métodos de la vista GUI reciben objetos
 * que implementen la interfaz IBeast. Por eso, para crear el ejemplo, era necesario
 * crear un objeto que implemente esa interfaz.
 */
public class BeastGUI(string name, int hp, int maxHp, int shields) 
    : IBeast
{
    public string Name { get; set; } = name;
    public int HP { get; set; } = hp;
    public int MaxHP { get; set; } = maxHp;
    public int Shields { get; set; } = shields;
}