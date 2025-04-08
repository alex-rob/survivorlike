using System;
using Godot;
using Survivorlike.characters.players;
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
    
    // Called when the node enters the scene tree for the first time.
    // TODO move this up to Weapon
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

        if (GetParent() is not Player player) throw new Exception("Parent node is null or is not a Player node");
        
        // Add the bullet to the tree
        GetTree().Root.AddChild(bullet);
        
        // Set the velocity vector and position
        bullet.SetGlobalPosition(GetGlobalPosition());

        if (AutoAim && AutoAimTarget != null)
        {
            target = AutoAimTarget.GetGlobalPosition();
            // Offset by the inverse of the player's velocity scaled by the affect constant if the player is moving
            target.Y = GetGlobalPosition().Y;
        }
        else
        {
            target = MouseTargetAtHeight(this, GetGlobalPosition().Y);
        }
        
        if (player.Velocity != Vector3.Zero)
        {
            var offset = player.Velocity * PlayerVelocityAffectProjectiles 
                                         * TimeToTarget(GetGlobalPosition(), target, bullet.TravelSpeed);
            target -= offset;
        }
        
        bullet.LookAt(target);
        
        // Init happens after the LookAt so that we have the correct rotation of the node
        bullet.Init(player);
        
        EmitSignalShotFired(bullet);
    }
}