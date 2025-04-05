using System.Collections.Generic;
using Godot;
using Survivorlike.components.attacks;

namespace Survivorlike.components.attack_manager;

public partial class AttackManager : Node3D
{
    private List<Weapon> _attacks = [];
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        foreach (Weapon atk in _attacks)
        {
            
        }
    }

    public bool RegisterAttack(Weapon w)
    {
        if (w != null)
        {
            _attacks.Add(w);
        }
        return true;
    }
}