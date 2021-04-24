using Godot;
using System;

public class Pepper : Node2D {
    private const int NUMBER_BEATS = 4;

    [Export(PropertyHint.Range, "1,3")]
    public int Speed { get; set; } = 1; // lower is faster
    
    private HeartBeat _heartBeat;
    private AnimationPlayer _animationPlayer;
    
    public override void _Ready() {
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

        _animationPlayer = this.GetNode<AnimationPlayer>();
    }
    
    public void OnHeartBeat(int beat) {
        var frame = (beat / Speed) % NUMBER_BEATS;
        _animationPlayer.Play($"Beat{frame}");
    }
}