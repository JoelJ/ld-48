using Godot;
using System;

public static class GodotUtils {
    /// <summary>
    /// Finds the 
    /// </summary>
    /// <param name="self"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetNode<T>(this Node2D self) where T : Node {
        return self.GetNode<T>(typeof(T).Name);
    }
}
