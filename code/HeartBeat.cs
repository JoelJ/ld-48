using Godot;
using System;

public class HeartBeat : Node2D {
    [Signal]
    public delegate void OnHeartBeat(int beat);

    [Export] public float TimeBetweenBeatsSeconds { get; set; } = 0.5f;

    public int CurrentBeat { get; private set; }
    private float _timeSinceLastBeat;

    public override void _Ready() {
        GD.Print("Loaded HeartBeat");
    }

    public override void _PhysicsProcess(float delta) {
        _timeSinceLastBeat += delta;

        if (_timeSinceLastBeat >= TimeBetweenBeatsSeconds) {
            _timeSinceLastBeat -= TimeBetweenBeatsSeconds;
            CurrentBeat += 1;
            EmitSignal(nameof(OnHeartBeat), CurrentBeat);
        }
    }
}