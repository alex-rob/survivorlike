using Godot;

namespace Survivorlike.components.attacks;

public partial class FriendlyAttack : Area3D
{
    protected Node ParentNode;
    public virtual void Init(Node parentNode)
    {
        ParentNode = parentNode;
    }
}