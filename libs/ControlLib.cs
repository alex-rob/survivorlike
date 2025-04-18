using System;
using System.Collections.Generic;
using static Survivorlike.libs.StarMath;

namespace Survivorlike.libs;

using Godot;

public static class ControlLib
{
    public const float PlayerVelocityAffectProjectiles = 0.1f;
    
    /*
     * ScreenPointToRay(node) : Vector3
     *
     * Casts a ray from the mouse cursor position on screen up to a maximum distance (2000 units).
     * If a collision is found, returns the Vector3 of the position of the collision. Otherwise,
     * returns Vector3.Zero.
     *
     * For the node parameter, the node calling the method should pass itself.
     */
    private static Vector3 ScreenPointToRay(Node3D node)
    {
        // Get the current space state of the 3D world
        var spaceState = node.GetWorld3D().GetDirectSpaceState();
        
        // Need the mouse position on screen (x,y) and the camera Node
        var mousePos = node.GetViewport().GetMousePosition();
        // GD.Print("mousePos = ", mousePos);
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

    /// <summary>
    /// Returns a vector along the line from the position that the mouse is pointing at to the
    /// mouse itself in global space as relative to the active Camera3D. If <paramref name="targetY"/>
    /// is zero, just returns the mouse pointing position. May behave strangely if the position the
    /// mouse points at does not have a Y component of zero.
    /// </summary>
    /// <param name="node">Reference point node</param>
    /// <param name="targetY">Y component of the vector to be returned</param>
    /// <returns>Vector3 of the point along the line at the given height</returns>
    public static Vector3 MouseTargetAtHeight(Node3D node, float targetY = 0f)
    {
        // Get the global mouse position: https://www.youtube.com/watch?v=jvxeHSotKpg
        var mousePointPos = ScreenPointToRay(node);
        
        // If targetY is zero then we can skip executing the rest of the function
        if (targetY == 0f) return mousePointPos;
        
        // Need the camera to get the ray origin of the mouse position
        var camera = node.GetTree().GetRoot().GetCamera3D();

        // Get the positions relating to the mouse pointing on screen.
        var mousePosOnScreen = node.GetViewport().GetMousePosition();
        var mouseVector = camera.ProjectRayOrigin(mousePosOnScreen);

        // Get the vector from the pointing position to the position of mouse relative to camera in global space
        var targetToMouse = mouseVector - mousePointPos;
        // Doing cartesian form shenanigans, we know our targetY already so we can solve for lambda
        var lambda = (targetY - mousePointPos.Y) / targetToMouse.Y;

        // Fill in the rest to solve for X and Z
        var targetX = mousePointPos.X + lambda * targetToMouse.X;
        var targetZ = mousePointPos.Z + lambda * targetToMouse.Z;
        
        return new Vector3(targetX, targetY, targetZ);
    }

    /// <summary>
    /// This method is useful for calculating approximate travel time in a straight line from one node
    /// to a target
    /// </summary>
    /// <param name="nodePos"></param>
    /// <param name="targetPos"></param>
    /// <param name="travelSpeed"></param>
    /// <returns></returns>
    public static float TimeToTarget(Vector3 nodePos, Vector3 targetPos, float travelSpeed)
    {
        var distance = (targetPos - nodePos).Length();
        return distance / travelSpeed;
    }
    
    
    public static List<Vector3> GetFixedSpreadTargets(int num, float degSpread, Vector3 originGlobal, Vector3 targetGlobal)
    {
        float radSpread = DegToRad(degSpread);
        List<Vector3> spreadTargets = [];
        Vector3 axis = Vector3.Up; // Rotation around the Y axis

        if (num == 0) return spreadTargets; // No num means empty spread
        
        var originToTarget = targetGlobal - originGlobal;
        
        // If odd # of bullets, first target is the original target. Otherwise, give negative half offset to the first bullet
        spreadTargets.Add(num % 2 == 1 ? targetGlobal : originToTarget.Rotated(axis, radSpread * -0.5f) + originGlobal);

        // keep a ref to the first target relative to the origin point
        var originToTargetZero = spreadTargets[0] - originGlobal;
        
        for (int i = 1; i < num; i++)
        {
            // calculate the factor based on the number of projectiles in the spread
            int factor = (i + 1) / 2;
            if (i % 2 == 0) factor *= -1;
            
            // Rotate the target vector relative to the origin point and convert it back to global space
            spreadTargets.Add(originToTargetZero.Rotated(axis, radSpread * factor) + originGlobal);
        }
        
        return spreadTargets;
    }

    public static List<Vector3> GetAreaSpreadTargets(int num, float degArea, Vector3 originGlobal, Vector3 targetGlobal)
    {
        return GetFixedSpreadTargets(num, degArea/num, originGlobal, targetGlobal);
    }
}