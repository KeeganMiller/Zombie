using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;

public partial class BuildModeController : Node2D
{
    public bool IsBuildMode = false;

    [ExportGroup("Walls")]
    [Export] private Node2D _PlacingObject;
    private bool _Rotated = false;

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        if (!IsBuildMode)
            return;

        if (_PlacingObject != null)
        {
            Vector2 mousePos = GetGlobalMousePosition();
            GridNode node = GetNode<GameController>("/root/GameController")?.Grid.GetNodeFromPosition(mousePos);
            SetPlacingObjectPosition(node);
        }

        if (Input.IsActionJustPressed("Rotate"))
            _Rotated = !_Rotated;

    }

    private void SetPlacingObjectPosition(GridNode node)
    {
        // Validate the placing object
        if (_PlacingObject == null)
            return;

        _PlacingObject.Position = node.CellPosition;

        if (_PlacingObject is WallController wall)
        {
            if(_Rotated)
                wall.ShowWall("Side");
            else 
                wall.ShowWall("Straight");
        }
    }

    public void SetPlacingObject(PackedScene node)
    {
        Node2D spawning = node.Instantiate<Node2D>();
        if (spawning != null)
        {
            GetNode<GameController>("/root/GameController").Grid.AddChild(spawning);
            _PlacingObject = spawning;
        }
            

    }
}