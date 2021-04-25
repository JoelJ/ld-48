using Godot;
using System;

public class Game : Node2D {
    private PackedScene _fogScene;

    private TileMap _floorTileMap;
    private Player _player;
    
    public override void _Ready() {
        _fogScene = ResourceLoader.Load<PackedScene>("res://sprites/static/Fog.tscn");

        _floorTileMap = GetNode<TileMap>("FloorTileMap");

        _player = this.GetNode<Player>();
        _player.SafeConnect(nameof(Player.StairsGet), this, nameof(OnStairsGet));

        var cells = _floorTileMap.GetUsedCells();
        foreach (Vector2 cellVector in cells) { // each vector is based ont he 
            var fogPosition = cellVector * _floorTileMap.CellSize;
            var fog = _fogScene.Instance<Fog>();
            fog.Position = fogPosition;
            AddChild(fog);
        }
    }

    public void OnStairsGet(string destination) {
        GD.Print("Stairs ", destination);
        QueueFree();
    }
}