using Godot;
using System;

public class ControllerAi : Node2D {
    public Direction Direction { get; private set; } = Direction.Down;
    
    private HeartBeat _heartBeat;
    
    public override void _Ready() {
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));
    }

    public override void _PhysicsProcess(float delta) {
        var direction = ReadInput();
        if (direction != Direction.None) {
            Direction = direction;
        }
    }
    
    public void OnHeartBeat(int _) {
        Direction = Direction.None;
    }
    
    private Direction ReadInput() {
        if (Input.IsActionJustPressed("up")) {
            return Direction.Up;
        }

        if (Input.IsActionPressed("down")) {
            return Direction.Down;
        }

        if (Input.IsActionPressed("left")) {
            return Direction.Left;
        }

        if (Input.IsActionPressed("right")) {
            return Direction.Right;
        }

        return Direction.None;
    }
}