using System;
using System.Collections.Generic;
using System.Linq;
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
    private static readonly List<Direction> ALL_DIRECTIONS = new List<Direction> {
        Direction.Up,
        Direction.Down,
        Direction.Left,
        Direction.Right
    };
    
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

    public static Direction RandomValue(this Direction self, int maxNumberOfAttempts = 5) {
        if (self != Direction.None) {
            for (var i = 0; i < maxNumberOfAttempts; i++) { // if we can't find a direction after X attempts, then just give up.
                var index = SfxPlayer.RANDOM.Next(0, ALL_DIRECTIONS.Count);
                var maybeDirection = ALL_DIRECTIONS[index];
                if (self.HasFlag(maybeDirection)) {
                    return maybeDirection;
                }
            }
        }

        return Direction.None;
    }
}