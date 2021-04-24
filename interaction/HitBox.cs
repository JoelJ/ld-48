using Godot;
using System;

public class HitBox : Area2D {
    public IRoot Root { get; private set; }
    
    public override void _Ready() {
        Root = this.FindRoot();
    }
}