using System;
using Godot;

public class Stats : Node2D, IStats {
    private PackedScene _fadingLabelScene;
    private PackedScene _kaboomScene;
    
    [Export(PropertyHint.Range, "10,255")] public int MaxHealth { get; set; } = 10;
    public int RemainingHealth { get; set; } = 10;

    [Export(PropertyHint.Range, "1,255")] public int Strength { get; set; } = 1;
    [Export(PropertyHint.Range, "1,255")] public int Defense { get; set; } = 1;
    
    [Export(PropertyHint.Range, "1,255")] public int Accuracy { get; set; } = 1;
    [Export(PropertyHint.Range, "1,255")] public int Evasion { get; set; } = 1;

    public IRoot Root { get; private set; }
    
    public override void _Ready() {
        _fadingLabelScene = ResourceLoader.Load<PackedScene>($"res://gameplay/TemporaryLabel.tscn");
        _kaboomScene = ResourceLoader.Load<PackedScene>($"res://gameplay/Kaboom.tscn");
        RemainingHealth = MaxHealth;
        Root = this.FindRoot();
    }

    public void Heal(int amount) {
        RemainingHealth = Mathf.Clamp(RemainingHealth + amount, 0, MaxHealth);
        GD.Print(Root.Name(), " healed by ", amount, " -> ", RemainingHealth);

        var label = _fadingLabelScene.Instance<TemporaryLabel>();
        label.Text = $"+{amount}";
        label.TextColor = Colors.Cyan;
        label.Effect = TemporaryLabel.TextEffect.Wave;
        label.GlobalPosition = Root.AsNode().GlobalPosition;
        Root.AsNode().GetParent().AddChild(label);
    }

    public void Hurt(int amount) {
        RemainingHealth = Mathf.Clamp(RemainingHealth - amount, 0, MaxHealth);
        GD.Print(Root.Name(), " hurt by ", amount, " -> ", RemainingHealth);

        var toAddChildTo = Root.AsNode().GetParent();

        var label = _fadingLabelScene.Instance<TemporaryLabel>();
        label.Text = $"-{amount}";
        label.TextColor = Colors.Red;
        label.Effect = TemporaryLabel.TextEffect.Shake;
        label.GlobalPosition = Root.AsNode().GlobalPosition;
        toAddChildTo.AddChild(label);

        if (RemainingHealth <= 0) {
            var kaboom = _kaboomScene.Instance<Kaboom>();
            kaboom.GlobalPosition = Root.AsNode().GlobalPosition;
            toAddChildTo.AddChild(kaboom);
            Root.Die();
        }
    }

    public void Miss() {
        var label = _fadingLabelScene.Instance<TemporaryLabel>();
        label.Text = "Miss!";
        label.TextColor = Colors.Limegreen;
        label.Effect = TemporaryLabel.TextEffect.Tornado;
        label.GlobalPosition = Root.AsNode().GlobalPosition;
        Root.AsNode().GetParent().AddChild(label);
    }
}