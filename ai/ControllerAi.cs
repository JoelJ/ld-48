using Godot;
using System;

public class ControllerAi : Node2D {
    private HeartBeat _heartBeat;
    private Node2D _parent;
    
    public override void _Ready() {
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

        _parent = this.FindRoot().AsNode();
    }

    public void OnHeartBeat(int _) {
        var direction = ReadInput();
        GD.Print(direction);
        
        var vector = direction.AsVector2() * 32;
        _parent.GlobalPosition += vector;
    }
    
    private Direction ReadInput() {
        if (Input.IsActionPressed("up")) {
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