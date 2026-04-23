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
    }

    public void ShowGameStateTeams(List<string> teamsState)
    {
        _view.WriteLine(_separator);
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
        _view.WriteLine(_separator);
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
    public void ShowVictimsAvailableMessageForTraveler(List<string> victims, string travelerName)
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Seleccione un objetivo para " + travelerName);
        int optionNumber = 1;
        foreach (string victim in victims)
        {
            _view.WriteLine(optionNumber + ": " + victim);
            optionNumber++;
        }
        _view.WriteLine(optionNumber + ": Cancelar");
    }
    public int GetVictimOptionForTraveler()
    {
        return int.Parse(_view.ReadLine());
    }
    
    private void AskHowManyBoostPointsToUse()
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Seleccione cuantos BP utilizar");
    }
    public int GetHowManyBoostPointsToUse()
    {
        AskHowManyBoostPointsToUse();
        return int.Parse(_view.ReadLine());
    }
    private void ShowSkillsAvailableMessage(List<string> skills, string travelerName)
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Seleccione una habilidad para " + travelerName);
        int optionNumber = 1;
        foreach (string skill in skills)
        {
            _view.WriteLine(optionNumber + ": " + skill);
            optionNumber++;
        }
        _view.WriteLine(optionNumber + ": Cancelar");
    }
    private void ShowListOfFilesForTeams(List<string> files)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        for (int i = 0; i < files.Count; i++)
        {
            _view.WriteLine($"{i}: {files[i]}");
        }
    }
    public int GetFileOptionForTeams(List<string> files)
    {
        ShowListOfFilesForTeams(files);
        return int.Parse(_view.ReadLine());
    }
    public int GetSkillOptionChoosen(List<string> skills, string travelerName)
    {
        ShowSkillsAvailableMessage(skills, travelerName);
        return int.Parse(_view.ReadLine());
    }
    public void ShowBeastAtack(string BeastName, string victimName, int damage, string skillName, int victimHp, string skillType)
    {
        _view.WriteLine(_separator);
        _view.WriteLine(BeastName + " usa " + skillName);
        _view.WriteLine(victimName + " recibe " + damage + " de daño " + skillType);
        ShowFinalHp(victimName, victimHp);
    }
    public void ShowBasicAttackResultMessage(string name, string victimName, string weapon, int damage, int victimHp)
    {
        _view.WriteLine(_separator);
        _view.WriteLine(name + " ataca");
        _view.WriteLine(victimName + " recibe " + damage + " de daño de tipo " + weapon);
    }
    public void ShowBasicAttackWithWeaknessResultMessage(string name, string victimName, string weapon, int damage, int victimHp)
    {
        _view.WriteLine(_separator);
        _view.WriteLine(name + " ataca");
        _view.WriteLine(victimName + " recibe " + damage + " de daño de tipo " + weapon + " con debilidad");
    }

    public void ShowFinalHp(string victimName, int victimHp)
    {
        _view.WriteLine(victimName + " termina con HP:" + victimHp);
    }
    public void ShowSkillDamageResult(string victimName, string skillType, int damage)
    {
        _view.WriteLine(victimName + " recibe " + damage + " de daño de tipo " + skillType);
    }
    public void ShowSkillDamageResultWithWeakness(string victimName, string skillType, int damage)
    {
        _view.WriteLine(victimName + " recibe " + damage + " de daño de tipo " + skillType + " con debilidad");
    }
    public void ShowSkillUsed(string atacante, string skillName)
    {
        _view.WriteLine(_separator);
        _view.WriteLine(atacante + " usa " + skillName);
    }
    public void ShowHealingResult(string targetName, int healAmount)
    {
        _view.WriteLine(targetName + " recupera " + healAmount + " de vida");
    }
    public void ShowLegholdTrapEffect(string victimName, int roundsInLastPlace)
    {
        _view.WriteLine(victimName + " tendrá menor prioridad de turno durante " + roundsInLastPlace + " rondas");
    }

    public void ShowReviveResult(string unitRevived)
    {
        _view.WriteLine(unitRevived + " revive");
    }
    
    
    public void ShowBreakingPoint(string victimName)
    {
        _view.WriteLine(victimName + " entra en Breaking Point");
    }
    public void ShowRunAwayMessage()
    {
        _view.WriteLine(_separator);
        _view.WriteLine("El equipo de viajeros ha huido!");
    }
    public void ShowDefetedTravelerMessage()
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Gana equipo del enemigo");
    }
    public void ShowVictoryTravelerMessage()
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Gana equipo del jugador");
    }

    private void ShowAllysAvailableMessage(List<string> allysNames, string travelerName)
    {
        _view.WriteLine(_separator);
        _view.WriteLine("Seleccione un objetivo para " + travelerName);
        int optionNumber = 1;
        foreach (string name in allysNames)
        {
            _view.WriteLine(optionNumber + ": " + name);
            optionNumber++;
        }
        _view.WriteLine(optionNumber + ": Cancelar");
    }

    private int GetAllyOption()
    {
        return int.Parse(_view.ReadLine());
    }
    // funciones de ataque de la bestia
    public void ShowBeastDamage(string victimName, int damage, string attackType)
    {
        _view.WriteLine(victimName + " recibe " + damage + " de daño " + attackType);
    }
    public void ShowBeastDamageWithVortalClaw(string victimName, int damage)
    {
        _view.WriteLine(victimName + " recibe " + damage + " de daño");
    }
    // funcion de accion defenderse
    public void ShowTravelerDefending(string travelerName)
    {
        _view.WriteLine(_separator);
        _view.WriteLine(travelerName + " se defiende");
    }
    public int GetAllyChoosen(List<string> allyNames, string travelerName)
    {
        ShowAllysAvailableMessage(allyNames, travelerName);
        return GetAllyOption();
    }
    public string[] GetScript()
        => _view.GetScript();
}