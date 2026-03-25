namespace Octopath_Traveler_View;

public class View
{
    private readonly string _separator = "----------------------------------------";
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
        _view.WriteLine(_separator);
        _view.WriteLine("INICIA RONDA " + round);
        _view.WriteLine(_separator);
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
            _view.WriteLine(indicadorDeTurno + "." + unit);
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
            _view.WriteLine(indicadorDeTurno + "." + unit);
            indicadorDeTurno++;
        }
    }

    public void ShowOptionsTavelerMessage(string unitName, List<string> travelerOptions)
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Turno de " + unitName);
        int optionNumber = 1;
        foreach (string option in travelerOptions)
        {
            _view.WriteLine(optionNumber + ": " + option);
            optionNumber++;
        }
    }
    private void ShowWeaponsAvailableMessage(List<string> weapons)
    {
        _view.WriteLine("Seleccione un arma");
        int optionNumber = 1;
        foreach (string weapon in weapons)
        {
            _view.WriteLine(optionNumber + ": " + weapon);
            optionNumber++;
        }
        _view.WriteLine(optionNumber + ": Cancelar");
    }
    public int GetWeaponOption(List<string> weapons)
    {
        ShowWeaponsAvailableMessage(weapons);
        return int.Parse(_view.ReadLine());
    }
    
    public void ShowSkillsAvailableMessage(List<string> skills)
    {
        _view.WriteLine("Seleccione una habilidad");
        int optionNumber = 1;
        foreach (string skill in skills)
        {
            _view.WriteLine(optionNumber + ": " + skill);
            optionNumber++;
        }
        _view.WriteLine(optionNumber + ": Cancelar");
    }
    public void ShowBeastAtack(string BeastName, string victimName, int damage, string skillName, int victimHP)
    {
        _view.WriteLine(BeastName + " usa " + skillName);
        _view.WriteLine(victimName + " recibe " + damage + " de daño fisico");
        _view.WriteLine(victimName + " termina con HP:" + victimHP);
    }
    public string[] GetScript()
        => _view.GetScript();
}