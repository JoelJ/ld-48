using Godot;
using System;

public class Pepper : Node2D, IRoot {
    [Export(PropertyHint.Range, "1,3")]
    public int Speed { get; set; } = 1; // lower is faster
    
    private HeartBeat _heartBeat;
    private AnimationPlayer _animationPlayer;
    
    public IStats Stats { get; private set; }
    
    public override void _Ready() {
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

        _animationPlayer = this.GetNode<AnimationPlayer>();
        
        Stats = this.GetNode<Stats>();
    }
    
    public void OnHeartBeat(int beat) {
        var frame = (beat / Speed) % 4;
        _animationPlayer.Play($"Beat{frame}");
    }
}