using Godot;
using System;

public class Game : Node2D {
    private PackedScene _fogScene;

    private TileMap _floorTileMap;
    
    public override void _Ready() {
        _fogScene = ResourceLoader.Load<PackedScene>("res://sprites/static/Fog.tscn");

        _floorTileMap = GetNode<TileMap>("FloorTileMap");

        var cells = _floorTileMap.GetUsedCells();
        foreach (Vector2 cellVector in cells) { // each vector is based ont he 
            var fogPosition = cellVector * _floorTileMap.CellSize;
            var fog = _fogScene.Instance<Fog>();
            fog.Position = fogPosition;
            AddChild(fog);
        }
    }
}