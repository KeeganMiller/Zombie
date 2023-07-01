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

            if (Input.IsActionJustPressed("LeftMouseClicked"))
            {
                if (_PlacingObject is BuildingController building)
                {
                    UpdateGridMap(building);
                }
                _PlacingObject = null;
            }
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

    private void UpdateGridMap(BuildingController building)
    {
        if (building != null)
        {
            GridController grid = GetNode<GameController>("/root/GameController").Grid;
            int CellX = building.BuildingSizeX;
            int CellY = building.BuildingSizeY;
            Vector2 buildingPosition = building.GlobalPosition;
            GridNode placedOverNode = grid.GetNodeFromPosition(buildingPosition);
            if (placedOverNode != null)
            {
                for (int y = 0; y < CellY; ++y)
                {
                    for (int x = 0; x < CellX; ++x)
                    {
                        // Deteremine the cell position to update
                        int nextCellX = placedOverNode.CellIndexX + x;
                        int nextCellY = placedOverNode.CellIndexY + y;
                        // Update the grid cell
                        grid.Grid[nextCellX, nextCellY].SetPlacedObject(building, building._IsCellWalkable(x, y));
                    }
                }
            }
            else
            {
                GD.PrintErr("Failed to get node");
            }
        }
    }
}