using OctopathTravelerGUI.Models.Interfaces;

namespace Octopath_Traveler.ClassesGUI;

/*
 * Esta es una clase auxiliar. Ocurre que los métodos de la vista GUI reciben objetos
 * que implementen la interfaz ITraveler. Por eso, para crear el ejemplo, era necesario
 * crear un objeto que implemente esa interfaz.
 */
public class TravelerGUI(string name, int hp, int maxHp, int sp, int maxSp, int boostPoints)
    : ITraveler
{
    public string Name { get; set; } = name;
    public int HP { get; set; } = hp;
    public int MaxHP { get; set; } = maxHp;
    public int SP { get; set; } = sp;
    public int MaxSP { get; set; } = maxSp;
    public int BoostPoints { get; set; } = boostPoints;
}