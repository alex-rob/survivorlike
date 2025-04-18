using System;
using Godot;
using Survivorlike.characters.players;
using Survivorlike.libs;

namespace Survivorlike.components.attacks.beam;

public partial class Beam : Weapon
{
    private const float SpreadAngle = 360f;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        CooldownTime = 0.75f;
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    // Spawn a laser as a child of the beam weapon so movement is tracked.
    private void CreateLaser(Vector3 target)
    {
        var laser = AttackScene.Instantiate<Laser>();
        AddChild(laser);

        laser.LookAt(target);
        
        EmitSignalShotFired(laser);
    }

    protected override void LaunchAttack()
    {
        if (GetParent() is not Player player) throw new Exception("Parent node is null or is not a Player node");
        
        var aimTarget = GetAimTarget();

        var spreadTargets = ControlLib.GetAreaSpreadTargets(Version, SpreadAngle,
            GetGlobalPosition(), aimTarget);

        foreach (var target in spreadTargets)
        {
            CreateLaser(target);
        }
    }
}