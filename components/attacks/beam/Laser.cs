using Godot;
using Survivorlike.characters.enemies;

namespace Survivorlike.components.attacks.beam;

public partial class Laser : FriendlyAttack
{
    private float _damage = 15f;
    private float _timeToKill = 2f;
    
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Create a HitBaudRate timer to check for hits at a specified rate
        var hitBaudRateTimer = new Timer();
        hitBaudRateTimer.WaitTime = 0.05f; // 20Hz scan rate
        hitBaudRateTimer.Autostart = true;
        hitBaudRateTimer.Timeout += Scan;
        AddChild(hitBaudRateTimer);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        
    }

    private void OnBodyEntered(Node3D node)
    {
        TickDamage(node);
    }

    private void Scan()
    {
        // get each overlapping body
        // for each body, first return if there is a LaserHitCooldown attached
        // if no cooldown attached, check if the node is an enemy
        // if the node is an enemy, deal damage and attach a LaserHitCooldown
        var hits = GetOverlappingBodies();
        if (hits == null) return;

        foreach (var node in hits)
        {
            TickDamage(node);
        }
    }

    private void TickDamage(Node3D node)
    {
        if (!node.IsInGroup("shootable") || node.HasNode("LaserHitCooldown")) return;

        if (node.IsInGroup("enemy"))
        {
            ((EnemyEntity)node).TakeDamage(_damage);
            AttachCooldownTimer(node);
        }
    }

    private static void AttachCooldownTimer(Node3D node)
    {
        var hitCooldown = new Timer();
        hitCooldown.Name = "LaserHitCooldown";
        hitCooldown.WaitTime = 0.5f;
        hitCooldown.Autostart = true;
        hitCooldown.Timeout += () => hitCooldown.Free();
        node.AddChild(hitCooldown);
    }
}