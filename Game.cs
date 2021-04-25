using Godot;
using System;

public class Game : Node2D {
    private PackedScene _fogScene;

    private TileMap _floorTileMap;
    private Player _player;
    private Node2D _level;
    
    public override void _Ready() {
        _fogScene = ResourceLoader.Load<PackedScene>("res://sprites/static/Fog.tscn");

        _level = GetNode<Node2D>("Level");
        InitializeLevel();
    }

    private void InitializeLevel() {
        _floorTileMap = _level.GetNode<TileMap>("FloorTileMap");

        _player = _level.GetNode<Player>();
        _player.SafeConnect(nameof(Player.StairsGet), this, nameof(OnStairsGet));

        var cells = _floorTileMap.GetUsedCells();
        foreach (Vector2 cellVector in cells) { // each vector is based ont he 
            var fogPosition = cellVector * _floorTileMap.CellSize;
            var fog = _fogScene.Instance<Fog>();
            fog.Position = fogPosition;
            _level.AddChild(fog);
        }
    }

    public void OnStairsGet(string destination) {
        GD.Print("Stairs ", destination);
        CallDeferred(nameof(ChangeLevel), destination);
        
    }

    private void ChangeLevel(string level) {
        _level.QueueFree();
        
        var nextLevelScene = ResourceLoader.Load<PackedScene>(level);
        _level = nextLevelScene.Instance<Node2D>();
        AddChild(_level);
        InitializeLevel();
    }
}