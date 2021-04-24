using Godot;
using System;
using System.Collections.Generic;

public class SfxPlayer : Node2D {
    public static readonly Random RANDOM = new Random();
    
    [Export(PropertyHint.Range, "0, 99")]
    public int Volume { get; set; } = 80;
    
    private List<AudioStreamPlayer> _audioStreamPlayer;
    
    public override void _Ready() {
        _audioStreamPlayer = new List<AudioStreamPlayer>();

        foreach (var child in GetChildren()) {
            if (child is AudioStreamPlayer audio) {
                _audioStreamPlayer.Add(audio);
            }
        }
    }

    public void PlayRandom() {
        var index = RANDOM.Next(0, _audioStreamPlayer.Count);
        var audioStreamPlayer = _audioStreamPlayer[index];

        audioStreamPlayer.VolumeDb = Volume - 80;
        audioStreamPlayer.Play();
    }
}