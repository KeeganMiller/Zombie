using System;
using System.Collections.Generic;
using System.Diagnostics;
using Godot;

public partial class BuildModeController : Node2D
{
    public bool IsBuildMode = false;

    [ExportGroup("Walls")]
    [Export] private Node2D _PlacingObject;
    private PackedScene _LastPlacedObject;
    private bool _Rotated = false;

    private bool _CanPlaceBuilding = true;

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
    }

    private void SetPlacingObjectPosition(GridNode node)
    {
        // Validate the placing object
        if (_PlacingObject == null || node == null)
            return;

        _PlacingObject.Position = node.CellPosition;                // Update the objects position
    }


    public void SetPlacingObject(PackedScene node, bool repeatBuilding = false)
    {
        Node2D spawning = node.Instantiate<Node2D>();
        if (spawning != null)
        {
            GetNode<GameController>("/root/GameController").Grid.AddChild(spawning);
            _PlacingObject = spawning;
            if (repeatBuilding)
                _LastPlacedObject = node;
            else
                _LastPlacedObject = null;
        }
    }
}