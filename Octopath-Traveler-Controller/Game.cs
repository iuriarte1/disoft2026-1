using Octopath_Traveler_View;
using Octopath_Traveler_Model;
using Octopath_Traveler.Controllers;
using Octopath_Traveler.Data;
namespace Octopath_Traveler;

public class Game
{
    private View _view;
    private readonly string _teamsFolder;

    private List<Traveler> _travelersDatabase = new();
    private List<Beast> _beastsDatabase = new();
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        string selectedTeamFile = EncargadoDeArchivosParaTest();
        LoadDatabases();

        var teamBuilder = new TeamBuilderFromPath(_view, _travelersDatabase, _beastsDatabase);
        var teams = teamBuilder.BuildTeams(selectedTeamFile);

        // Si el archivo de equipos fue inválido, detener la ejecución inmediatamente.
        if (!teams.IsValid) return;

        // --- ¡AQUÍ ESTÁ LA MAGIA! ---
        // Le entregamos los equipos validados al CombatManager y arrancamos el juego
        
        var combatManager = new CombatManager(_view, teams.Travelers, teams.Beasts);
        combatManager.StartBattle();
    }
    private void LoadDatabases()
    {
        var loader = new JsonInfoLoader();

        // IMPORTANTE: Revisa que estos sean los nombres exactos de los archivos JSON de tu tarea
        _travelersDatabase = loader.LoaderTravelers("data/characters.json");
        _beastsDatabase = loader.LoaderBeasts("data/enemies.json");
    }
    private void ImprimirListaDeArchivos(List<string> archivos)
    {
        for (int i = 0; i < archivos.Count; i++)
        {
            _view.WriteLine($"{i}: {archivos[i]}");
        }
    }
    private string EncargadoDeArchivosParaTest()
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        var archivos = new FolderHandler().GainFolderNames(_teamsFolder);
        ImprimirListaDeArchivos(archivos);
        var input = int.Parse(_view.ReadLine());
        var ruta = Path.Combine(_teamsFolder, archivos[input]);
        return ruta;
    }
}
