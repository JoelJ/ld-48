using Godot;
using System;

[Tool]
public class Light : Area2D {
    private float _radius = 2;

    private CircleShape2D _circleShape;

    [Export(PropertyHint.Range, "1, 100")]
    public float Radius {
        get => _radius;
        set {
            _radius = value;
            if (_circleShape != null) {
                _circleShape.Radius = value * 32;
            }

            Update();
        }
    }

    [Export(PropertyHint.Range, "0.0,1.0")]
    public float Intensity { get; set; } = 1.0f;

    public override void _Ready() {
        _circleShape = this.GetNode<CollisionShape2D>().Shape as CircleShape2D ??
                       throw new Exception("Lights should use a Circle shape");
        _circleShape.Radius = Radius * 32;
    }
}