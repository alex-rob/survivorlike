using Godot;

namespace Survivorlike.components.attacks;

public partial class FriendlyAttack : Area3D
{
    protected CharacterBody3D ParentNode;
    
    /// <summary>
    /// Assigns the ParentNode of the FriendlyAttack. Should be overridden by
    /// any class inheriting from FriendlyAttack to add functionality to the
    /// attack.
    /// </summary>
    /// <param name="parentNode"></param>
    public virtual void Init(CharacterBody3D parentNode)
    {
        ParentNode = parentNode;
    }
}