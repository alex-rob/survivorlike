using Godot;

namespace Survivorlike.components.attacks;

public partial class Weapon : Node3D
{
    private bool _autoAim;
    public Node3D AutoAimTarget { get; set; }

    public virtual void LaunchAttack(Vector3 target)
    { }
}