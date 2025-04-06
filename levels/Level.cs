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

    private Node3D _nearestTargetToPlayer;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        GD.Print("Entered Level _Ready");
        _player = GetNode<Player>("Player"); // TODO replace this with a FindClass(Player) once there are more player characters
        if (_player == null) throw new NullReferenceException("Player is null");
        FindAllChildrenOfClass(this, ref _enemies);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _PhysicsProcess(double delta)
    {
        if (_player == null) return;
        
        // Cast the list of enemies to Node3D
        var nearestEnemy = FindNearestToTarget(_player, _enemies.Cast<Node3D>().ToList());

        // If the nearest enemy is still the nearest, don't update the player's target
        if (nearestEnemy == null || nearestEnemy == _nearestTargetToPlayer) return;

        _nearestTargetToPlayer = nearestEnemy;
        _player.SetAutoAimTarget(nearestEnemy);
    }
}
