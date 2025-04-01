namespace Survivorlike.libs;

using Godot;

public static class ControlLib
{
    /*
     * ScreenPointToRay(node) : Vector3
     *
     * Casts a ray from the mouse cursor position on screen up to a maximum distance (2000 units).
     * If a collision is found, returns the Vector3 of the position of the collision. Otherwise,
     * returns Vector3.Zero.
     *
     * For the node parameter, the node calling the method should pass itself.
     */
    public static Vector3 ScreenPointToRay(Node3D node)
    {
        // Get the current space state of the 3D world
        var spaceState = node.GetWorld3D().GetDirectSpaceState();
        
        // Need the mouse position on screen (x,y) and the camera Node
        var mousePos = node.GetViewport().GetMousePosition();
        var camera = node.GetTree().GetRoot().GetCamera3D();
        
        // Creating the ray, we first get the origin by projecting from the camera by the mousePos on screen...
        var rayOrigin = camera.ProjectRayOrigin(mousePos);
        // ... then, we project that to a ridiculous distance to make sure the ray hits something.
        var rayEnd = rayOrigin + camera.ProjectRayNormal(mousePos) * 2000;

        var query = PhysicsRayQueryParameters3D.Create(rayOrigin, rayEnd);
        var rayArray = spaceState.IntersectRay(query);
        if (rayArray.ContainsKey("position"))
        {
            return (Vector3)rayArray["position"];
        }
        return Vector3.Zero;
    }

    public static Vector3 GetMouseShootTarget(Node3D node)
    {
        // Get the global mouse position: https://www.youtube.com/watch?v=jvxeHSotKpg
        Vector3 mousePointPos = ScreenPointToRay(node);
        
        // Add the current global Y transform to the mouse point position
        float worldHeight = node.GlobalPosition.Y;
        return new Vector3(mousePointPos.X, mousePointPos.Y + worldHeight, mousePointPos.Z);
    }
}