namespace Octopath_Traveler_View;

public class View
{
    private readonly string _separator = "----------------------------------------\n";
    private readonly AbstractView _view;

    public static View BuildConsoleView()
        => new View(new ConsoleView());

    public static View BuildTestingView(string pathTestScript)
        => new View(new TestingView(pathTestScript));

    public static View BuildManualTestingView(string pathTestScript)
        => new View(new ManualTestingView(pathTestScript));
    
    private View(AbstractView newView)
    {
        _view = newView;
    }
    
    public string ReadLine() => _view.ReadLine();
    
    public void WriteLine(string message)
    {
        _view.WriteLine(message);
    }

    public void InvalidTeamsFileMessage()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }

    public void ShowRoundMessage(int round)
    {
        _view.WriteLine(_separator + $"INICIA RONDA {round}" + _separator);
    }

    public void ShowGameStateTeams(List<string> teamsState)
    {
        foreach (string unitState in teamsState)
        {
            _view.WriteLine(unitState);
        }
    }
    public void ShowTurnsMessage(List<string> actualTurns, List<string> futureTurns)
    {
        ShowActualTruns(actualTurns);
        ShowFutureTruns(futureTurns);
    }
    private void ShowActualTruns(List<string> actualTruns)
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Turnos de la ronda");
        int indicadorDeTurno = 1;
        foreach (string unit in actualTruns)
        {
            _view.WriteLine(indicadorDeTurno + ". " + unit);
                indicadorDeTurno++;
        }
    }
    private void ShowFutureTruns(List<string> FutureTruns)
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Turnos de la siguiente ronda");
        int indicadorDeTurno = 1;
        foreach (string unit in FutureTruns)
        {
            _view.WriteLine(indicadorDeTurno + ". " + unit);
            indicadorDeTurno++;
        }
    }
    public string[] GetScript()
        => _view.GetScript();
}