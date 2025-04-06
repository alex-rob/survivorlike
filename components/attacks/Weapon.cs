using Godot;

namespace Survivorlike.components.attacks;

public partial class Weapon : Node3D
{
    public bool AutoAim { get; set; }
    public Node3D AutoAimTarget { get; set; }

    public virtual void LaunchAttack()
    { }
}