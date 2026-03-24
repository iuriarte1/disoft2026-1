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
        
        var teamBuilder = new TeamBuilderFromPath(_view, _travelersDatabase, _beastsDatabase);
        // Intentamos construir los equipos
        var teams = teamBuilder.BuildTeams(selectedTeamFile);
        if (teams == null)
        {
            _view.InvalidTeamsFileMessage();

        }
        _view.WriteLine("----------------------------------------");
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