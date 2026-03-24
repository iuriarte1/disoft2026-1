using Octopath_Traveler_Model;
using Octopath_Traveler_View;

namespace Octopath_Traveler.Data;

public class TeamBuilderFromPath
{
    private readonly View _view;
    private readonly List<Traveler> _travelersDatabase;
    private readonly List<Beast> _beastsDatabase;
    private readonly string _enemylineIdentifier = "Enemy Team";
    private readonly string _travelerIdentifier = "Player Team";
    private string _currentLineType = "Player";
    private List<string> PlayerLines = new();
    private List<string> EnemyLines = new();

    public TeamBuilderFromPath(View view, List<Traveler> travelersDatabase, List<Beast> beastsDatabase)
    {
        _view = view;
        _travelersDatabase = travelersDatabase;
        _beastsDatabase = beastsDatabase;
    }

    // Ahora además retorna IsValid para que el llamador pueda detenerse si es inválido
    public (List<Traveler> Travelers, List<Beast> Beasts, bool IsValid) BuildTeams(string filePath)
    {
        ExtractLinesFromFile(filePath);

        var travelerParser = new TravelerParser(_view, _travelersDatabase);
        List<Traveler> playerTeam = travelerParser.ParseTeam(PlayerLines) ?? new List<Traveler>();

        var beastParser = new BeastParser(_view, _beastsDatabase);
        List<Beast> enemyTeam = beastParser.ParseTeam(EnemyLines) ?? new List<Beast>();

        var validator = new TeamValidationService(_view);
        bool travelersOk = validator.ValidateTravelers(playerTeam);
        bool beastsOk = validator.ValidateBeasts(enemyTeam);

        bool isValid = travelersOk && beastsOk;
        return (playerTeam, enemyTeam, isValid);
    }

    private void ExtractLinesFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string cleanLine = line.Trim();
            if (string.IsNullOrWhiteSpace(cleanLine)) continue;
            if (cleanLine == "Player Team" || cleanLine == "Enemy Team")
            {
                ChangeCurrentLineType(line);
            }
            else
            {
                AddTravelerLines(cleanLine);
                AddEnemyLines(cleanLine);
            }
        }
    }
    private void ChangeCurrentLineType(string line)
    {
        if (line == _travelerIdentifier)
        {
            _currentLineType = "Player";
        }
        else if (line == _enemylineIdentifier)
        {
            _currentLineType = "Enemy";
        }
    }
    private void AddTravelerLines(string line)
    {
        if (_currentLineType == "Player")
        {
            PlayerLines.Add(line);
        }
    }
    private void AddEnemyLines(string line)
    {
        if (_currentLineType == "Enemy")
        {
            EnemyLines.Add(line);
        }
    }
}
