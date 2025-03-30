using Godot;

namespace Survivorlike;

/*
 * StatusEffect class
 *
 * This is the generic base class 
 */
public class StatusEffect
{
    public Vector3 ApplyMoveEffect(Vector3 velocity) { return _moveEffect(velocity); }

    protected virtual Vector3 _moveEffect(Vector3 velocity)
    {
        return velocity;
    }
    
    // Add more status effect processing methods here
}