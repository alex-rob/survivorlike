using Godot;
using Survivorlike.components.health_component;

namespace Survivorlike.characters.enemies;

internal static class Constants
{
    public const float MaxHealth = 100f;
}

public abstract partial class EnemyEntity : CharacterBody3D
{
    [Signal] public delegate void DeathEventHandler();
    private HealthComponent _healthComponent = new(Constants.MaxHealth);

    public override void _ExitTree()
    {
        _healthComponent.QueueFree();
    }

    /// <summary>
    /// Handles playing an animation when damage is taken. Base does nothing,
    /// should be overridden by any inheriting class depending on desired
    /// animation.
    /// </summary>
    protected virtual void PlayHurtAnimation()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if (!_healthComponent.TakeDamage(damage))
        {
            PlayHurtAnimation();
            return;
        }

        EmitSignalDeath(); // TODO change this to some sort of animation or other stuff that needs to happen on death   
    }
}