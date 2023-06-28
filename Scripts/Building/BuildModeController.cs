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

        if (_PlacingObject is WallController wall)
        {
            // Cycle buiding forward on input
            if (Input.IsActionJustPressed("CycleBuildingForward"))
                wall.CycleWallForward();
            
            // Cycle building backwards on input
            if(Input.IsActionJustPressed("CycleBuildingBackwards"))
                wall.CycleWallBackwards();

            // On mouse click
            if (Input.IsActionJustPressed("LeftMouseClicked"))
            {
                // Check that an object hasn't already been placed their
                if (node.PlacedObject == null)
                {
                    // Set the placed object
                    node.SetPlacedObject(_PlacingObject, wall.GetWalkable());
                    // Check if we are repeating the action
                    if (_LastPlacedObject != null)
                        SetPlacingObject(_LastPlacedObject, true);
                }
            }
            
            
        }
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