using Godot;
using System;

public static class GodotUtils {
    /// <summary>
    /// Gets the child node of the given node that has a name matching the name of the generic type provided.   
    /// </summary>
    /// <param name="self">The node to check the children of.</param>
    /// <typeparam name="T">The type and name of the child to retrieve.</typeparam>
    /// <returns>The found child node.</returns>
    public static T GetNode<T>(this Node2D self) where T : Node {
        var name = typeof(T).Name;
        return self.GetNode<T>(name) ?? throw new Exception($"Unable to find child node with name '{name}' for node '{self.Name}'");
    }
    
    public static void SafeConnect(this Node source, string sourceSignal, Node target, string targetMethod) {
        var connectResult = source.Connect(sourceSignal, target, targetMethod);
        if (connectResult != Error.Ok) {
            throw new Exception($"could not connect {source.Name}.{sourceSignal ?? "{null}"} to {target?.Name ?? "{null}"}.{targetMethod ?? "{null}"}: {connectResult}");
        }
    }
}
