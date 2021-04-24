using Godot;
using System;

public class BattleSystem : Node2D {
    private Random _random;

    public override void _Ready() {
        _random = new Random();
    }

    public void Fight(IRoot aggressor, IRoot defender) {
        var chanceToDodge = Mathf.Clamp((defender.Stats.Evasion - aggressor.Stats.Accuracy) / 255.0f, 0.0f, 1.0f);
        
        var miss = _random.NextDouble() < chanceToDodge;
        if (miss) {
            GD.Print(aggressor.Name(), " missed ", defender.Name(), " with a ", chanceToDodge, " chance to miss");
            aggressor.Stats.Miss();
            return;
        }

        var damage = aggressor.Stats.Strength - defender.Stats.Defense;
        defender.Stats.Hurt(damage);
        
        GD.Print(aggressor.Name(), " hit ", defender.Name(), " for ", damage, " with a ", chanceToDodge, " chance to miss");
    }
}