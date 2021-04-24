using Godot;
using System;

public class WanderAi : Node2D {
    private BattleSystem _battleSystem;
    private HeartBeat _heartBeat;
    
    private IRoot _root;
    private Vision _vision;
    
    public override void _Ready() {
        _battleSystem = GetNode<BattleSystem>("/root/BattleSystem");
        
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

        _root = this.FindRoot();
        _vision = _root.AsNode().GetNode<Vision>() ?? throw new Exception("Blind wandering is not currently supported");
    }
    
    public void OnHeartBeat(int beat) {
        if (beat % 4 != 2) {
            // Player moves/attacks on the down beat, so enemies can move/attack opposite of that
            return;
        }

        if (_vision.HasEnemyInSight(out var enemy)) {
            _battleSystem.Fight(_root, enemy);
        } else {
            var directionsCanGo = _vision.Unblocked;
            var randomDirection = directionsCanGo.RandomValue();
        
            var vector = randomDirection.AsVector2() * 32;
            _root.AsNode().GlobalPosition += vector;
        }
    }
}