using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class ControllerAi : Node2D {
    private BattleSystem _battleSystem;
    private HeartBeat _heartBeat;
    
    private IRoot _root;
    private Vision _vision;

    private SfxPlayer _hitWallSfx;
    
    
    public override void _Ready() {
        _battleSystem = GetNode<BattleSystem>("/root/BattleSystem");
        
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

        _root = this.FindRoot();
        _vision = _root.AsNode().GetNode<Vision>();

        _hitWallSfx = this.GetNode<SfxPlayer>();
    }

    public void OnHeartBeat(int _) {
        var direction = ReadInput();

        if (direction != Direction.None) {
            var enemiesInSight = _vision.EnemiesInSight(direction);
            if (enemiesInSight.Count > 0) {
                var defender = enemiesInSight.First();
                GD.Print(_root.Name(), " attacking ", defender.Name());

                _battleSystem.Fight(_root, defender);
            } else if (_vision.CanMove(direction)) {
                var vector = direction.AsVector2() * 32;
                _root.AsNode().GlobalPosition += vector;
            } else {
                _hitWallSfx.PlayRandom();
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