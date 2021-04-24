using Godot;
using System.Collections.Generic;
using System.Linq;

public class Vision : Node2D {
    private const Direction ALL_DIRECTIONS = Direction.Up | Direction.Down | Direction.Left | Direction.Right;
    public Direction Blocked { get; private set; }

    public Direction Unblocked => ALL_DIRECTIONS & ~Blocked;

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

    /// <summary>
    /// Finds if an enemy is in sight. Since this AI is going on baddies, and in the context of a baddie, the enemy is the player
    /// then we just return the first one, since there is only ever going to be one. 
    /// </summary>
    /// <param name="result">if the method call returns false, this will be null. Otherwise it will be the found enemy if vision</param>
    /// <returns>true if an enemy is found.</returns>
    public bool HasEnemyInSight(out IRoot result) {
        foreach (var enemies in _enemiesInSight.Values) {
            foreach (var enemy in enemies) {
                result = enemy;
                return true;
            }
        }

        result = null;
        return false;
    }
    
    public bool CanMove(Direction direction) {
        return (Blocked & direction) != direction;
    }
}