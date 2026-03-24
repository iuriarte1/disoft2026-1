using System;
using System.Collections.Generic;
using System.Linq;
using Octopath_Traveler_Model;
using Octopath_Traveler_View;

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
        // Regla de Octopath: El orden se define por Speed (Cap. 2: Nombres significativos)
        List<Unit> turnOrder = GetTurnOrder();

        foreach (Unit unit in turnOrder)
        {
            if (unit.CurrentHp <= 0) continue; // Los caídos no actúan
            if (!IsBattleOngoing()) break;     // Si la pelea termina a mitad de ronda

            ProcessUnitTurn(unit);
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
        _view.WriteLine($"\nTurno de {traveler.Name} (HP: {traveler.CurrentHp}/{traveler.BaseStats.MaxHp}, SP: {traveler.CurrentSp})");
        
        // Aquí llamarías a un método de tu View para mostrar el menú de opciones
        // Por ahora simularemos la elección básica de atacar al primer enemigo vivo
        Beast target = _enemyTeam.First(b => b.CurrentHp > 0);
        PerformPhysicalAttack(traveler, target);
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