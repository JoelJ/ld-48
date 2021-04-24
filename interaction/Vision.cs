using Godot;
using System.Collections.Generic;

public class Vision : Node2D {
    public Direction Blocked { get; private set; }

    private Dictionary<Direction, HashSet<IRoot>> _enemiesInSight;
    private IRoot _root;
    
    public override void _Ready() {
        _root = this.FindRoot();
        
        _enemiesInSight = new Dictionary<Direction, HashSet<IRoot>> {
            {Direction.Up, new HashSet<IRoot>()},
            {Direction.Down, new HashSet<IRoot>()},
            {Direction.Left, new HashSet<IRoot>()},
            {Direction.Right, new HashSet<IRoot>()}
        };
    }

    public void OnEnemyEntered(Area2D body, int directionBit) {
        var direction = (Direction) directionBit;
        if (body is HitBox hb) {
            _enemiesInSight[direction].Add(hb.Root);
        }
    }
    
    public void OnEnemyExited(Area2D body, int directionBit) {
        var direction = (Direction) directionBit;
        if (body is HitBox hb) {
            _enemiesInSight[direction].Remove(hb.Root);
        }
    }

    public void OnObstacleEntered(Node body, int directionBit) {
        var direction = (Direction) directionBit;
        Blocked |= direction;
    }

    public void OnObstacleExited(Node body, int directionBit) {
        var direction = (Direction) directionBit;
        Blocked &= ~direction;
    }

    public HashSet<IRoot> EnemiesInSight(Direction direction) {
        return _enemiesInSight[direction];
    }
    
    public bool CanMove(Direction direction) {
        return (Blocked & direction) != direction;
    }
}