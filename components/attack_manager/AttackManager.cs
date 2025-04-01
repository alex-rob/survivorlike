using System.Collections.Generic;
using Godot;
using Survivorlike.components.attacks;

namespace Survivorlike.components.attack_manager;

public partial class AttackManager : Node3D
{
    private List<Attack> _attacks = [];
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        foreach (Attack atk in _attacks)
        {
            
        }
    }

    public bool RegisterAttack(Attack atk)
    {
        if (atk != null)
        {
            _attacks.Add(atk);
        }
        return true;
    }
}