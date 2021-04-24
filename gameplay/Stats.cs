using System;
using Godot;

public class Stats : Node2D, IStats {
    [Export(PropertyHint.Range, "10,255")] public int MaxHealth { get; set; } = 10;
    public int RemainingHealth { get; set; } = 10;

    [Export(PropertyHint.Range, "1,255")] public int Strength { get; set; } = 1;
    [Export(PropertyHint.Range, "1,255")] public int Defense { get; set; } = 1;
    
    [Export(PropertyHint.Range, "1,255")] public int Accuracy { get; set; } = 1;
    [Export(PropertyHint.Range, "1,255")] public int Evasion { get; set; } = 1;

    public IRoot Root { get; private set; }
    
    public override void _Ready() {
        RemainingHealth = MaxHealth;

        Root = this.FindRoot();
    }

    public void Heal(int amount) {
        RemainingHealth = Mathf.Clamp(RemainingHealth + amount, 0, MaxHealth);
        GD.Print(Root.Name(), " healed by ", amount, " -> ", RemainingHealth);
    }

    public void Hurt(int amount) {
        RemainingHealth = Mathf.Clamp(RemainingHealth - amount, 0, MaxHealth);
        GD.Print(Root.Name(), " hurt by ", amount, " -> ", RemainingHealth);

        if (RemainingHealth <= 0) {
            Root.Die();
        }
    }

    public void Miss() {
        
    }
}