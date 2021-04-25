using Godot;
using System;

public class Fog : Node2D {
    public void OnLightEnters(Area2D body) {
        if (body is Light lightSource) {
            Modulate = new Color(Modulate,  Mathf.Min(Modulate.a, 1.0f - lightSource.Intensity));
        }
    }
}