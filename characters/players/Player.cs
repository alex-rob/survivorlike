using System.Collections.Generic;
using Godot;
using Survivorlike.characters.enemies;
using Survivorlike.components.attacks;
using static Survivorlike.libs.EntityLib;

namespace Survivorlike.characters.players;

public partial class Player : CharacterBody3D
{
    [Export] private int Speed { get; set; } = 14;
    [Export] private Camera3D Camera { get; set; }
    
    private List<Weapon> _weapons = [];
    private bool _autoAim;
    private EnemyEntity _autoAimTarget;

    

    private List<StatusEffect> _statusEffects = []; // Makes a default blank list with new() constructor

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FindAllChildrenOfClass(this, ref _weapons);


        foreach (var w in _weapons)
        {
            w.AddVersion(3); // Here for debugging increased versions
            RegisterWeaponToPlayer(w);
        }
        
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("toggle_auto_aim"))
        {
            _autoAim = !_autoAim;
            foreach (Weapon w in _weapons) w.AutoAim = _autoAim;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var inputDirection = Vector3.Zero;

        if (Input.IsActionPressed("move_right"))
        {
            // x+
            inputDirection.X += 1.0f;
        }

        if (Input.IsActionPressed("move_left"))
        {
            // x-
            inputDirection.X -= 1.0f;
        }
        
        if (Input.IsActionPressed("move_up"))
        {
            // z-
            inputDirection.Z -= 1.0f;
        }

        if (Input.IsActionPressed("move_down"))
        {
            // z+
            inputDirection.Z += 1.0f;
        }

        if (inputDirection != Vector3.Zero)
        {
            inputDirection= inputDirection.Normalized();
        }
        
        Velocity = inputDirection * Speed;
        
        // Include all other velocity processing that modifies the actual velocity after target
        // velocity is acquired through input direction. This could include gravity, slow/haste,
        // random direction modifiers, etc.
        //
        // Could in the future make a set of scenes that are status effects that attach themselves
        // to the player for certain periods of time that each carry a movement altering effect.
        
        MoveAndSlide();
    }

    public void RegisterWeaponToPlayer(Weapon w)
    {
        if (w.GetParent() != this) AddChild(w);
        w.ShotFired += (atk) => atk.Init(this);
        w.SetPosition(new Vector3(0, 1, 0));
    }

    public void SetAutoAimTarget(EnemyEntity target)
    {
        _autoAimTarget = target;
        foreach (Weapon w in _weapons)
        {
            w.AutoAimTarget = target;
        }

        target.Death += () =>
        {
            
            if (_autoAimTarget != target) return;
            
            _autoAimTarget = null;
            foreach (Weapon w in _weapons)
            {
                w.AutoAimTarget = null;
            }

        };
    }
}