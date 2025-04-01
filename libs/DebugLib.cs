namespace Survivorlike.libs;

using Godot;

public static class DebugLib
{
    private const bool Debug = true;
    
    /// <summary>
    /// Prints a debug string to the console if <c>Debug</c>
    /// </summary>
    /// <param name="str"></param>
    public static void DebugPrintStr(string str)
    {
        if (Debug)
        {
            GD.Print("Debug Log: ", str);
        }
    }
}