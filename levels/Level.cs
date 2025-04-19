using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Survivorlike.characters.enemies;
using Survivorlike.characters.players;
using static Survivorlike.libs.EntityLib;

namespace Survivorlike.levels;

public partial class Level : Node3D
{
    [Export] private Player _player;
    private List<EnemyEntity> _enemies = [];

    private EnemyEntity _nearestTargetToPlayer;
    
    private float _spawnRate = 1f; // TODO make this tied to difficulty over time?
    private int _maxEnemySpawns = 20f;

    [Signal] public delegate void FreeOrphanedEnemiesEventHandler();

    public override void _ExitTree()
    {
        EmitSignalFreeOrphanedEnemies();
    }

    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        GD.Print("Entered Level _Ready");
        _player = GetNode<Player>("Player"); // TODO replace this with a FindClass(Player) once there are more player characters
        if (_player == null) throw new NullReferenceException("Player is null");
        
        // Find all enemies placed on the map (likely from the editor) and register them
        FindAllChildrenOfClass(this, ref _enemies);
        foreach (var e in _enemies) RegisterEnemy(e);

		// Create a timer that spawns enemeies every _spawnRate interval.
		var spawnTimer = new Timer();
		spawnTimer.WaitTime = _spawnRate;
		spawnTimer.Autostart = True;

		var spawnTimer = new Timer();
        spawnTimer.WaitTime = _hitScanBaudRate;
        spawnTimer.Autostart = true;
        spawnTimer.Timeout += Scan;
        AddChild(spawnTimer);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// TODO _Process
		// [ ] Come up with formulas that update with scaling difficulty over time
		// 	   for _spawnRate and _maxEnemySpawns (as well as anything else that
		// 	   might scale over time  
	}

    public override void _PhysicsProcess(double delta)
    {
        if (_player == null) return;
        
        // Cast the list of enemies to Node3D
        var nearestEnemy = FindNearestEnemyToTarget(_player, _enemies);

        // If the nearest enemy is still the nearest, don't update the player's target
        if (nearestEnemy == null || nearestEnemy == _nearestTargetToPlayer) return;

        _nearestTargetToPlayer = nearestEnemy;
        _player.SetAutoAimTarget(nearestEnemy);
    }

    private void RegisterEnemy(EnemyEntity enemy)
    {
        if (!_enemies.Contains(enemy)) _enemies.Add(enemy);
        enemy.Death += () =>
        {
            _enemies.Remove(enemy);
            RemoveChild(enemy);
            enemy.QueueFree();
        };
    }
}
