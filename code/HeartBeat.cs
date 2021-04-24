using Godot;
using System;

public class HeartBeat : Node2D {
    [Signal]
    public delegate void OnHeartBeat(int beat);

    public float TimeBetweenBeatsSeconds { get; } = 0.5f;
    public int MaxBeatsPerCycle { get; } = int.MaxValue; // might need to figure out a better value if we're syncing to music. at least we don't overflow...

    public int CurrentBeat { get; private set; }
    private float _timeSinceLastBeat;

    public override void _Ready() {
        GD.Print("Loaded HeartBeat");
    }

    public override void _PhysicsProcess(float delta) {
        _timeSinceLastBeat += delta;

        if (_timeSinceLastBeat >= TimeBetweenBeatsSeconds) {
            // reset the time by subtracting the max. 
            // this way if the time doesn't line up with the framerate
            // then it should auto-correct
            _timeSinceLastBeat -= TimeBetweenBeatsSeconds;
            
            CurrentBeat += 1;
            if (CurrentBeat >= MaxBeatsPerCycle) {
                CurrentBeat = 0;
            }
            
            EmitSignal(nameof(OnHeartBeat), CurrentBeat);
        }
    }
}