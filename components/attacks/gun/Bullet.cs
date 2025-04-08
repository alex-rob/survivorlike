using Godot;
using static Survivorlike.libs.DebugLib;
using static Survivorlike.libs.EntityLib;
using static Survivorlike.libs.ControlLib;

namespace Survivorlike.components.attacks.gun;

public partial class Bullet : FriendlyAttack
{
    [Export] public float TravelSpeed = 35f;
    [Export] private float _damage = 10f;
    [Export] private float _timeToKill = 10f;

    private Vector3 _velocity = Vector3.Forward;
    private Vector3 _originVelocity = Vector3.Zero;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        AttachKillTimer(this, _timeToKill);

        _velocity *= TravelSpeed;
    }

    public override void _PhysicsProcess(double delta)
    {
        TranslateObjectLocal(_velocity * (float)delta);
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

    public override void Init(CharacterBody3D parentNode)
    {
        var parentVelocity = parentNode.Velocity * PlayerVelocityAffectProjectiles;
        var angle = Rotation.Y;

        parentVelocity = parentVelocity.Rotated(Vector3.Up, angle);
        
        _velocity += parentVelocity;
        
        base.Init(parentNode);
    }
    
}