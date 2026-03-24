using Octopath_Traveler_View;
using Octopath_Traveler_Model;
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

        var teamBuilder = new TeamBuilderFromPath(_view, _travelersDatabase,_beastsDatabase);
        var teams = teamBuilder.BuildTeams(selectedTeamFile);

        // Si el archivo de equipos fue inválido, detener la ejecución inmediatamente.
        if (!teams.IsValid) return;

        _view.WriteLine("----------------------------------------");

        if (teams.Travelers.Count > 0)
        {
            _view.WriteLine("Viajeros parseados:");
            foreach (var t in teams.Travelers)
            {
                _view.WriteLine($"- {t.Name}");
            }
        }
        else
        {
            _view.WriteLine("No se pudieron parsear viajeros.");
        }

        if (teams.Beasts.Count > 0)
        {
            _view.WriteLine("Bestias parseadas:");
            foreach (var b in teams.Beasts)
            {
                _view.WriteLine($"- {b.Name}");
            }
        }
        else
        {
            _view.WriteLine("No se pudieron parsear bestias.");
        }
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
