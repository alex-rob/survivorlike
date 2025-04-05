using Godot;
using static Survivorlike.libs.DebugLib;
using static Survivorlike.libs.EntityLib;

namespace Survivorlike.components.attacks.gun;

public partial class Bullet() : Area3D
{
    [Export] private float _travelSpeed = 35f;
    [Export] private float _damage = 10f;
    [Export] private float _timeToKill = 10f;
    private Node _parentNode;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        // AttachKillTimer(this, _timeToKill);
        AttachTimedEvent(this, _timeToKill, () =>
        {
            DebugPrintStr("Bullet removed");
            QueueFree();
        });
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
        if (node == _parentNode) return;

        if (node.IsInGroup("shootable"))
        {
            if (node.IsInGroup("enemy"))
            {
                // call the enemy's hurt function passing the damage component of the bullet
            }
            
            DebugPrintStr("Bullet hit enemy");
            QueueFree();
        }
    }

    public void Init(Node parentNode)
    {
        _parentNode = parentNode;
    }
}