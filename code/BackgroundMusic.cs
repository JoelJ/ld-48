using Godot;
using System;

public class BackgroundMusic : Node2D {
    private HeartBeat _heartBeat;

    private SfxPlayer _notesSfx;
    private SfxPlayer _snareSfx;
    private SfxPlayer _bassSfx;
    
    public override void _Ready() {
        _notesSfx = GetNode<SfxPlayer>("NotesSfxPlayer");
        _snareSfx = GetNode<SfxPlayer>("SnareSfxPlayer");
        _bassSfx = GetNode<SfxPlayer>("BassSfxPlayer");
        
        _heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
        _heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));
    }
    
    public void OnHeartBeat(int beat) {
        if ((beat % 12) == 0) {
            _notesSfx.PlayRandom();
        }
        
        if ((beat % 12) == 6) {
            _notesSfx.PlayRandom();
        }

        if ((beat % 4) == 0) {
            _bassSfx.PlayRandom();
        }
        if ((beat % 4) == 2) {
            _snareSfx.PlayRandom();
        }
    }
}