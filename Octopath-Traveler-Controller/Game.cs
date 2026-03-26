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
        string selectedTeamFile = TestFilesManager();
        LoadDatabases();
        var teamBuilder = new TeamBuilderFromPath(_view, _travelersDatabase, _beastsDatabase);
        var teams = teamBuilder.BuildTeams(selectedTeamFile);
        if (!teams.IsValid) return;
        var combatManager = new CombatManager(_view, teams.Travelers, teams.Beasts);
        combatManager.StartBattle();
    }
    private void LoadDatabases()
    {
        var loader = new JsonInfoLoader();
        _travelersDatabase = loader.LoaderTravelersBd("data/characters.json");
        _beastsDatabase = loader.LoaderBeastsBd("data/enemies.json");
    }
    private string TestFilesManager()
    {
        var archivos = new FolderHandler().GetFolderNames(_teamsFolder);
        var input = _view.GetFileOptionForTeams(archivos);
        var ruta = Path.Combine(_teamsFolder, archivos[input]);
        return ruta;
    }
}
