using Godot;
using static Survivorlike.libs.ControlLib;

namespace Survivorlike.components.attacks.gun;

/// <summary>
/// This class provides logic for a bullet-type weapon. This should be
/// attached as a node to the parent entity.
/// </summary>
public partial class Gun : Weapon
{
    [Export] private Timer _cooldownTimer;
    [Export] private PackedScene _bulletScene;
    private Bullet _bullet; // could be that this needs to be simplified to Area3D
    public Vector3 Target {get; set;}
    
    
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Attach shoot method to cooldown timeout event
        _cooldownTimer.Timeout += () => LaunchAttack(Target);
        Target = Vector3.Zero;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    // Shoot a bullet towards the target
    public override void LaunchAttack(Vector3 target)
    {
        // Instantiate a bullet
        Bullet bullet = _bulletScene.Instantiate<Bullet>();
        bullet.Init(GetParent());
        
        // Add the bullet to the tree
        GetTree().Root.AddChild(bullet);
        
        // Set the velocity vector and position
        bullet.SetGlobalPosition(GetGlobalPosition());
        bullet.LookAt(Target);
    }
}