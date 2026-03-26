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
    private List<Traveler> _travelerInFile;
    private List<Beast> _beastInFile;
    private bool _isValidFile;

    public TeamBuilderFromPath(View view, List<Traveler> travelersDatabase, List<Beast> beastsDatabase)
    {
        _view = view;
        _travelersDatabase = travelersDatabase;
        _beastsDatabase = beastsDatabase;
    }
    public (List<Traveler> Travelers, List<Beast> Beasts, bool IsValid) BuildTeams(string filePath)
    {
        ParseUnitsFromFilePath(filePath);
        CheckIfFileIsValid();
        return (_travelerInFile, _beastInFile, _isValidFile);
    }
    private void ParseUnitsFromFilePath(string filePath)
    {
        ExtractLinesFromFile(filePath);
        var travelerParser = new TravelerParser(_view, _travelersDatabase);
        _travelerInFile = travelerParser.ParseTeam(PlayerLines) ?? new List<Traveler>();
        var beastParser = new BeastParser(_view, _beastsDatabase);
        _beastInFile= beastParser.ParseEnemyTeam(EnemyLines) ?? new List<Beast>();
    }
    private void CheckIfFileIsValid()
    {
        var validator = new TeamValidationService(_view);
        bool travelersOk = validator.ValidateTravelers(_travelerInFile);
        bool beastsOk = validator.ValidateBeasts(_beastInFile);
        _isValidFile = travelersOk && beastsOk;
        
    }
    private void ExtractLinesFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var cleanLine = line.Trim();
            if (string.IsNullOrWhiteSpace(cleanLine)) continue;
            if (cleanLine is "Player Team" or "Enemy Team")
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
