using System;
using System.Collections.Generic;
using Godot;
using Survivorlike.characters.enemies;

namespace Survivorlike.libs;

public static class EntityLib
{
    /// <summary>
    /// Attaches a temp timer to <paramref name="node"/> that will call the <paramref name="node"/>'s
    /// QueueFree() method on timeout.
    /// </summary>
    /// <param name="node">Node to attach the timer to</param>
    /// <param name="timeToKill">Time until <paramref name="node"/>'s QueueFree() is called</param>
    public static void AttachKillTimer(Node node, float timeToKill)
    {
        AttachTimedEvent(node, timeToKill, node.QueueFree);
    }

    /// <summary>
    /// Attaches a temp timer to <paramref name="node"/> that will call the provided callback method
    /// on timeout.
    /// </summary>
    /// <param name="node">Node to attach the timer to</param>
    /// <param name="time">Time until temp timer expires</param>
    /// <param name="callback">Method called on timer timeout</param>
    public static void AttachTimedEvent(Node node, float time, Action callback)
    {
        var timer = node.GetTree().CreateTimer(time);
        timer.Timeout += callback;
    }

    /// <summary>
    /// Returns the nearest node in a list of nodes to a provided target node. If no nodes
    /// are present in <paramref name="nodes"/>, returns null.
    /// </summary>
    /// <param name="target">Node against which distance is checked</param>
    /// <param name="nodes">Nodes to find the distance to target</param>
    /// <returns>Node that is the nearest in distance to the target</returns>
    public static EnemyEntity FindNearestEnemyToTarget(Node3D target, List<EnemyEntity> nodes)
    {
        if (nodes == null) return null;
        
        EnemyEntity nearest = null;
        var targetPos = target.GetGlobalPosition();

        var minDist = float.MaxValue;
        
        foreach (var node in nodes)
        {
            var nodePos = node.GetGlobalPosition();
            var dist = nodePos.DistanceTo(targetPos);
            if (dist < minDist)
            {
                nearest = node;
                minDist = dist;
            }
        }
        
        return nearest;
    }

    /// <summary>
    /// Finds all nodes recursively that match class type <typeparamref name="T"/>,
    /// starting with the <paramref name="parent"/> node. Nodes that are of
    /// class type <typeparamref name="T"/> are placed in the <paramref name="nodes"/>
    /// list.
    /// </summary>
    /// <param name="parent">First node to check recursively</param>
    /// <param name="nodes">Array to add the nodes to</param>
    /// <typeparam name="T">Class type of node to check for</typeparam>
    public static void FindAllChildrenOfClass<T>(Node parent, ref List<T> nodes) where T : Node3D
    {
        if (parent is T node3D)
            nodes.Add(node3D);
        
        foreach (Node child in parent.GetChildren())
        {
            FindAllChildrenOfClass(child, ref nodes);
        }
    }
}