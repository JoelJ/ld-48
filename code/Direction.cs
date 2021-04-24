using System;
using Godot;

[Flags]
public enum Direction {
    None = 0,
    Up = 1,
    Down = 2,
    Left = 4,
    Right = 8,
}

public static class DirectionUtils {
    public static Vector2 AsVector2(this Direction self) {
        var x = 0;
        var y = 0;

        if ((self & Direction.Up) == Direction.Up) {
            y -= 1;
        } 
        
        if ((self & Direction.Down) == Direction.Down) {
            y += 1;
        }

        if ((self & Direction.Left) == Direction.Left) {
            x -= 1;
        }
        
        if ((self & Direction.Right) == Direction.Right) {
            x += 1;
        }

        return new Vector2(x, y);
    }
}