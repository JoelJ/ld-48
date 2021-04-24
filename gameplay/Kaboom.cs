using Godot;
using System;
using System.Collections.Generic;

public class Kaboom : Node2D {
    private HeartBeat _heartBeat;
    private AnimationPlayer _animationPlayer;
    private SfxPlayer _explodeSfx;

    [Export(PropertyHint.Range, "1,3")]
    public int Speed { get; set; } = 1; // lower is faster

    private int _stage = -1;
    private int _beat; 
    
    public override void _Ready() {
        _animationPlayer = this.GetNode<AnimationPlayer>();
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

        _explodeSfx = this.GetNode<SfxPlayer>();
    }

    public void Play() {
        _stage = 0;
    }

    public void OnHeartBeat(int _) {
        if (_stage == 0) {
            if (_beat == 0) {
                _explodeSfx.PlayRandom();
            }
            
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