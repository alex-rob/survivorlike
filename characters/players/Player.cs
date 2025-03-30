using System.Collections.Generic;
using Godot;

namespace Survivorlike.characters;

public partial class Player : CharacterBody3D
{
    [Export] private int Speed { get; set; } = 14;
    [Export] private Camera3D Camera { get; set; }
    
    private Vector3 _targetVelocity = Vector3.Zero;

    private List<StatusEffect> _statusEffects = []; // Makes a default blank list with new() constructor

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
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

        _targetVelocity.X = inputDirection.X * Speed;
        _targetVelocity.Z = inputDirection.Z * Speed;
        
        // Include all other velocity processing that modifies the actual velocity after target
        // velocity is acquired through input direction. This could include gravity, slow/haste,
        // random direction modifiers, etc.
        //
        // Could in the future make a set of scenes that are status effects that attach themselves
        // to the player for certain periods of time that each carry a movement altering effect.
        
        Velocity = _targetVelocity;
        MoveAndSlide();
    }
}