using Godot;
using Survivorlike.components.health_component;
using Survivorlike.levels;

namespace Survivorlike.characters.enemies;

internal static class Constants
{
    public const float MaxHealth = 100f;
}

public abstract partial class EnemyEntity : CharacterBody3D
{
    [Signal] public delegate void DeathEventHandler();
    private HealthComponent _healthComponent = new(Constants.MaxHealth);

    public void TakeDamage(float damage)
    {
        if (!_healthComponent.TakeDamage(damage)) return;
        
        EmitSignalDeath(); // TODO change this to some sort of animation or other stuff that needs to happen on death
    }
}