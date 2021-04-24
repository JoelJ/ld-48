using Godot;
using System;

public class BattleSystem : Node2D {
    private Random _random;

    public override void _Ready() {
        _random = new Random();
    }

    public void Fight(IRoot aggressor, IRoot defender) {
        var chanceToDodge = Mathf.Clamp((defender.Stats.Evasion - aggressor.Stats.Accuracy) / 255.0f, 0.05f, 95.0f);
        
        var miss = _random.NextDouble() < chanceToDodge;
        if (miss) {
            GD.Print(aggressor.Name(), " missed ", defender.Name(), " with a ", chanceToDodge, " chance to miss");
            aggressor.Stats.Miss();
            return;
        }

        var damage = ApplyDefenseToDamage(aggressor.Stats.Strength, defender);
        defender.Stats.Hurt(damage);
        
        GD.Print(aggressor.Name(), " hit ", defender.Name(), " for ", damage, " with a ", chanceToDodge, " chance to miss");
    }

    private static int ApplyDefenseToDamage(int damage, IRoot defender) {
        var maxPercentageReduction = (int) ((defender.Stats.Defense / 255.0) * 100);

        var reducedBy = damage * (SfxPlayer.RANDOM.Next(0, maxPercentageReduction) / 100.0f);

        GD.Print("Reducing damage dealt to ", defender.Name(), " by ", reducedBy);
        return damage - (int) reducedBy;
    }
}