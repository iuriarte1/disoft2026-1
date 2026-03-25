using Octopath_Traveler_Model;
using Octopath_Traveler_View;
using Octopath_Traveler.Actions;

namespace Octopath_Traveler.Controllers;

public class CombatManager
{
    private readonly View _view;
    private readonly List<Traveler> _playerTeam;
    private readonly List<Beast> _enemyTeam;
    private int _roundCount = 1;

    public CombatManager(View view, List<Traveler> playerTeam, List<Beast> enemyTeam)
    {
        _view = view;
        _playerTeam = playerTeam;
        _enemyTeam = enemyTeam;
    }

    // Método principal: Orquesta el flujo de la batalla (Cap. 3: Una sola cosa)
    public void StartBattle()
    {
        while (IsBattleOngoing())
        {
            _view.ShowRoundMessage(_roundCount);
            ExecuteRound();
            _roundCount++;
        }

        DisplayFinalResult();
    }

    private bool IsBattleOngoing()
    {
        return _playerTeam.Any(t => t.CurrentHp > 0) && _enemyTeam.Any(b => b.CurrentHp > 0);
    }

    private void ExecuteRound()
    {
        // 1. Mostrar estado de los equipos (Usando tu TeamManager)
        var gameStateManager = new StateManager(_view, _playerTeam, _enemyTeam);
        gameStateManager.GameStateStatsMessage();

        // 2. TIMELINE: Usamos tu brillante idea del TurnManager
        var turnManager = new TurnManager(_playerTeam, _enemyTeam);
        
        List<Unit> currentTurns = turnManager.GetCurrentRoundTurns();
        List<Unit> nextTurns = turnManager.GetNextRoundTurns();

        // 3. Le pasamos a tu View las listas de nombres generadas por el TurnManager
        _view.ShowTurnsMessage(
            turnManager.GetTurnNames(currentTurns), 
            turnManager.GetTurnNames(nextTurns)
        );

        // 4. Ejecutar la acción de cada unidad en orden
        foreach (Unit unit in currentTurns)
        {
            if (unit.IsDead) continue; // Por si lo mataron en un turno anterior de esta misma ronda
            if (!IsBattleOngoing()) break;

            ProcessUnitTurn(unit);
        }
        // 5. Al final de la ronda, regenerar BP de los viajeros vivos
        foreach (var traveler in _playerTeam)
        {
            if (traveler.CurrentHp > 0)
            {
                traveler.CurrentBp++;
            }
        }
    }

    private List<Unit> GetTurnOrder()
    {
        // Combinamos ambos equipos y ordenamos por Speed descendente
        return _playerTeam.Cast<Unit>()
            .Concat(_enemyTeam.Cast<Unit>())
            .OrderByDescending(u => u.BaseStats.Speed)
            .ToList();
    }

    private void ProcessUnitTurn(Unit unit)
    {
        if (unit is Traveler traveler)
        {
            HandlePlayerTurn(traveler);
        }
        else if (unit is Beast beast)
        {
            HandleEnemyTurn(beast);
        }
    }

    // --- LÓGICA DEL JUGADOR ---

    private void HandlePlayerTurn(Traveler traveler)
    {
        // 1. Mostrar el menú ("1: Ataque básico", "2: Usar habilidad"...)
        _view.ShowOptionsTavelerMessage(traveler.Name, traveler.Optionsattack);
        
        // 2. Leer la decisión del jugador ("1", "2", "3" o "4")
        string choice = _view.ReadLine();

        // 3. Instanciar la estrategia elegida
        ICombatAction action;
        
        switch (choice)
        {
            case "1":
                action = new BasicAttackAction();
                break;
            case "2":
                action = new UseSkillAction();
                break;
            case "3":
                action = new DefendAction();
                break;
            case "4":
                action = new RunAwayAction();
                break;
            default:
                _view.WriteLine("Opción no válida. Por defecto se realizará un Ataque Básico.");
                action = new BasicAttackAction();
                break;
        }

        // 4. Ejecutar la acción elegida, pasándole todo el contexto
        action.Execute(traveler, _playerTeam, _enemyTeam, _view);
    }

    // --- LÓGICA DEL ENEMIGO (IA Simple) ---

    private void HandleEnemyTurn(Beast beast)
    {
        Traveler target = GetRandomLivingTraveler();
        if (target != null)
        {
            PerformPhysicalAttack(beast, target);
        }
    }

    private Traveler GetRandomLivingTraveler()
    {
        var livingTravelers = _playerTeam.Where(t => t.CurrentHp > 0).ToList();
        if (livingTravelers.Count == 0) return null;
        
        Random rand = new Random();
        return livingTravelers[rand.Next(livingTravelers.Count)];
    }

    // --- ACCIONES DE COMBATE (Cap. 3: Funciones pequeñas) ---

    private void PerformPhysicalAttack(Unit attacker, Unit defender)
    {
        // Fórmula básica: Daño = Atk - Def (mínimo 1 de daño)
        int damage = Math.Max(1, attacker.BaseStats.PhysicalAttack - defender.BaseStats.PhysicalDefense);
        
        defender.CurrentHp -= damage;
        if (defender.CurrentHp < 0) defender.CurrentHp = 0;

        _view.WriteLine($"{attacker.Name} ataca a {defender.Name} causando {damage} de daño.");
        
        if (defender.CurrentHp <= 0)
        {
            _view.WriteLine($"{defender.Name} ha sido derrotado.");
        }
    }

    private void DisplayFinalResult()
    {
        _view.WriteLine("\n========================");
        if (_playerTeam.Any(t => t.CurrentHp > 0))
        {
            _view.WriteLine("¡VICTORIA! El equipo ha sobrevivido.");
        }
        else
        {
            _view.WriteLine("DERROTA... Todos los viajeros han caído.");
        }
        _view.WriteLine("========================\n");
    }
}