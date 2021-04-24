using System.Linq;
using Godot;

public class ControllerAi : Node2D {
    private HeartBeat _heartBeat;
    private Node2D _parent;
    private Vision _vision;
    
    public override void _Ready() {
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

        _parent = this.FindRoot().AsNode();
        _vision = _parent.GetNode<Vision>();
    }

    public void OnHeartBeat(int _) {
        var direction = ReadInput();

        if (direction != Direction.None) {
            var enemiesInSight = _vision.EnemiesInSight(direction);
            if (enemiesInSight.Count > 0) {
                GD.Print("Attacking ", enemiesInSight.First().AsNode().Name);
            } else if (_vision.CanMove(direction)) {
                var vector = direction.AsVector2() * 32;
                _parent.GlobalPosition += vector;
            }
        }
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