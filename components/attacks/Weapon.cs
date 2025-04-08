using Godot;

namespace Survivorlike.components.attacks;

public partial class Weapon : Node3D
{
    public bool AutoAim { get; set; }
    public Node3D AutoAimTarget { get; set; }
    private FriendlyAttack _attack;
    
    [Signal] public delegate void ShotFiredEventHandler(FriendlyAttack attack);

    protected virtual void LaunchAttack()
    {
        EmitSignalShotFired(_attack);
    }
}