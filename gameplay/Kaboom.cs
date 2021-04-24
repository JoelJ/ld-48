using Godot;
using System;

public class Kaboom : Node2D {
    private HeartBeat _heartBeat;
    private AnimationPlayer _animationPlayer;
    
    [Export(PropertyHint.Range, "1,3")]
    public int Speed { get; set; } = 1; // lower is faster

    private int _stage;
    private int _beat; 

    public override void _Ready() {
        _animationPlayer = this.GetNode<AnimationPlayer>();
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));
    }

    public void OnHeartBeat(int _) {
        if (_stage == 0) {
            var frame = (_beat / Speed) % 4;
            _beat += 1;

            _animationPlayer.Play($"Beat{frame}");

            if (frame == 3) {
                _stage += 1;
            }
        } else if (_stage == 1) {
            _animationPlayer.Play($"Fade");
            _stage += 1;
        } else if (_stage == 2) {
            if (Modulate.a <= 0) {
                _stage += 1;
                QueueFree();
            }
        }
    }
}