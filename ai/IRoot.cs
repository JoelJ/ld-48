using System;
using Godot;

/// <summary>
/// Denotes the parent most object of a sprite.
/// The various AI controllers will walk up its parents until it discovers the instance that implements this interface.
/// </summary>
public interface IRoot {
    IStats Stats { get; }
    void Die();
}

public static class RootUtils {
    public static string Name(this IRoot self) {
        return self.AsNode().Name;
    }

    public static Node2D AsNode(this IRoot self) {
        return self as Node2D ??
               throw new Exception($"object implementing {nameof(IRoot)} but does not extend {nameof(Node2D)}");
    }
}
