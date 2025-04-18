using System;
using Godot;
using Survivorlike.characters.enemies;
using Survivorlike.characters.players;
using static Survivorlike.libs.ControlLib;

namespace Survivorlike.components.attacks;

public partial class Weapon : Node3D
{
    protected Timer CooldownTimer;
    protected float CooldownTime;
    public bool AutoAim { get; set; }
    public EnemyEntity AutoAimTarget { get; set; }
    [Export] protected PackedScene AttackScene;
    protected int Version = 1;
    
    [Signal] public delegate void ShotFiredEventHandler(FriendlyAttack attack);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        CooldownTimer = new Timer();
        CooldownTimer.WaitTime = CooldownTime;
        CooldownTimer.Autostart = true;
        CooldownTimer.Timeout += LaunchAttack;
    }

    public override void _ExitTree()
    {
        CooldownTimer.Free();
    }

    protected Vector3 GetAimTarget()
    {
        Vector3 target;

        if (!IsInstanceValid(AutoAimTarget)) AutoAimTarget = null;

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

        return target;
    }
    
    protected virtual void LaunchAttack()
    {
        
    }
}