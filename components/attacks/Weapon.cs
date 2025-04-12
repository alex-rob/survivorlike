using Godot;
using Survivorlike.characters.enemies;

namespace Survivorlike.components.attacks;

public partial class Weapon : Node3D
{
    [Export] protected Timer CooldownTimer;
    public bool AutoAim { get; set; }
    public EnemyEntity AutoAimTarget { get; set; }
    [Export] protected PackedScene AttackScene;
    protected int Version = 4;
    
    [Signal] public delegate void ShotFiredEventHandler(FriendlyAttack attack);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Attach shoot method to cooldown timeout event
        CooldownTimer.Timeout += LaunchAttack;
    }
    
    protected virtual void LaunchAttack()
    {
        
    }
}