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
    private Bullet _bullet;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Attach shoot method to cooldown timeout event
        _cooldownTimer.Timeout += LaunchAttack;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        
    }

    // Shoot a bullet towards the target
    protected override void LaunchAttack()
    {
        Vector3 target;
        
        // Instantiate a bullet
        Bullet bullet = _bulletScene.Instantiate<Bullet>();
        bullet.Init(GetParent());
        
        // Add the bullet to the tree
        GetTree().Root.AddChild(bullet);
        
        // Set the velocity vector and position
        bullet.SetGlobalPosition(GetGlobalPosition());

        if (AutoAim && AutoAimTarget != null)
        {
            target = AutoAimTarget.GetGlobalPosition();
            target.Y = GetGlobalPosition().Y;
        }
        else
        {
            target = MouseTargetAtHeight(this, GetGlobalPosition().Y);
        }
        
        bullet.LookAt(target);
        base.LaunchAttack();
    }
}