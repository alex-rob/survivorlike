using Godot;
using static Survivorlike.libs.ControlLib;

namespace Survivorlike.components.attacks.gun;

/// <summary>
/// This class provides logic for a bullet-type weapon. This should be
/// attached as a node to the parent entity.
/// </summary>
public partial class Gun : Node3D
{
    [Export] private Timer _cooldownTimer;
    [Export] private PackedScene _bulletScene;
    private Bullet _bullet; // could be that this needs to be simplified to Area3D
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Attach shoot method to cooldown timeout event
        _cooldownTimer.Timeout += LaunchBullet;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    // Shoot a bullet towards the cursor
    private void LaunchBullet()
    {
        // Find the target defined by the mouse
        // NOTE: Edit this later to include controller controls
        Vector3 shootTarget = GetMouseShootTarget(this);
        
        // We should always be getting a non-zero vector if the level is drawn correctly
        if (shootTarget == Vector3.Zero)
        {
            GD.PrintErr("GetMouseShootTarget() returned zero vector!");
        }

        // Instantiate a bullet
        Bullet bullet = _bulletScene.Instantiate<Bullet>();
        bullet.Init(this.GetParent());
        
        // Add the bullet to the tree
        GetTree().Root.AddChild(bullet);
        
        // Set the velocity vector and position
        bullet.SetGlobalPosition(GetGlobalPosition());
        bullet.LookAt(shootTarget);
    }
}