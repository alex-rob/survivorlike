using Godot;

namespace Survivorlike.components.health_component;

public partial class HealthComponent(float maxHealth) : Node
{
    private float _health = maxHealth;

    /// <summary>
    /// Reduces health by <c><paramref name="damage"/></c> amount.
    /// </summary>
    /// <param name="damage">Amount of damage to reduce health by.</param>
    /// <returns>True if health after damage is zero or less, false otherwise.</returns>
    public bool TakeDamage(float damage)
    {
        _health -= damage;
        return _health <= 0;
    }
}