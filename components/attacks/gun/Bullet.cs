using Godot;
using static Survivorlike.libs.DebugLib;
using static Survivorlike.libs.EntityLib;

namespace Survivorlike.components.attacks.gun;

public partial class Bullet() : FriendlyAttack
{
    [Export] private float _travelSpeed = 35f;
    [Export] private float _damage = 10f;
    [Export] private float _timeToKill = 10f;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        AttachKillTimer(this, _timeToKill);
    }

    public override void _PhysicsProcess(double delta)
    {
        TranslateObjectLocal(Vector3.Forward * _travelSpeed * (float)delta);
    }
    
    // remember to figure out all the ways in which we would need to remove this from the tree.
    // this includes excess distance or time-alive

    private void OnBodyEntered(Node3D node)
    {
        // Escape if contact is with parent
        if (node == ParentNode) return;

        if (!node.IsInGroup("shootable")) return;
        
        if (node.IsInGroup("enemy"))
        {
            // call the enemy's hurt function passing the damage component of the bullet
        }
            
        DebugPrintStr("Bullet hit enemy");
        QueueFree();
    }

    public override void Init(Node parentNode)
    {
        // TODO add all the velocity init stuff here, add parent velocity to this velocity
        base.Init(parentNode);
    }
    
}