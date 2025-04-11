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
    private const float MultiBulletSpreadDeg = 10f; // Spread for multiple bullets in degrees
    
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        
    }

    // Shoot a bullet towards the target
    protected override void LaunchAttack()
    {
        Vector3 target;
        if (GetParent() is not Player player) throw new Exception("Parent node is null or is not a Player node");

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

        var spreadTargets = GetSpreadTargets(Version, MultiBulletSpreadDeg, target);

        // Create a bullet for each version of the weapon.
        for (var i = 0; i < Version; i++)
        {
            // Instantiate a bullet
            var bullet = AttackScene.Instantiate<Bullet>();
            // Add the bullet to the tree
            GetTree().Root.AddChild(bullet);
            // Set the velocity vector and position
            bullet.SetGlobalPosition(GetGlobalPosition());
            
            // TODO check this out a bit more. It doesn't feel quite right rn.
            if (player.Velocity != Vector3.Zero)
            {
                var offset = player.Velocity * PlayerVelocityAffectProjectiles 
                                             * TimeToTarget(GetGlobalPosition(), spreadTargets[i], bullet.TravelSpeed);
                spreadTargets[i] -= offset;
            }
            
            bullet.LookAt(spreadTargets[i]);
            // Init happens after the LookAt so that we have the correct rotation of the node
            bullet.Init(player);
            
            EmitSignalShotFired(bullet);
        }
    }
}