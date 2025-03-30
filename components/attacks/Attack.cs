using System.Collections;
using Godot;

namespace Survivorlike.components.attacks;

public enum AttackType
{
    Melee,
    Bullet,
    Mortar,
    Beam,
    Field,
    Target
}

public struct AttackProperties(AttackType type, float damage = 0f, float range = 0f, float cooldown = 0f)
{
    public AttackType Type = type;
    public float Damage = damage;
    public float Range = range;
    public float Cooldown = cooldown;
}

public abstract partial class Attack : Node
{
    private AttackProperties _properties;

    public Attack(AttackProperties props)
    {
        _properties = props;
    }

    public Attack(AttackType type, float damage = 0f, float range = 0f, float cooldown = 0f)
    {
        _properties = new AttackProperties(type, damage, range, cooldown);
    }

    // Include behavior for creation and deletion events to introduce more complexity depending on attack
}