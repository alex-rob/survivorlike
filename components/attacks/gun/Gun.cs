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
    private const float MultiBulletSpreadDeg = 8f; // Spread for multiple bullets in degrees

    public override void _Ready()
    {
        CooldownTime = 0.25f;
        base._Ready();
    }

    private void ShootBullet(Player player, Vector3 target)
    {
        // Instantiate a bullet
        var bullet = AttackScene.Instantiate<Bullet>();
        // Add the bullet to the tree
        GetTree().Root.AddChild(bullet);
        // Set the velocity vector and position
        bullet.SetGlobalPosition(GetGlobalPosition());
            
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

    // Shoot a bullet towards the target
    protected override void LaunchAttack()
    {
        if (GetParent() is not Player player) throw new Exception("Parent node is null or is not a Player node");
        
        var target = GetAimTarget();

        var spreadTargets = GetFixedSpreadTargets(Version, MultiBulletSpreadDeg, GetGlobalPosition(), target);

        // Create a bullet for each version of the weapon.
        for (var i = 0; i < Version; i++)
        {
            ShootBullet(player, spreadTargets[i]);
        }
    }
}