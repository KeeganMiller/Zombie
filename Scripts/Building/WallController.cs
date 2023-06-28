using Godot;
using System;
using System.Collections.Generic;

public partial class WallController : BuildingController
{
    private List<Sprite2D> _WallObjects = new List<Sprite2D>();                 // List of all the wall sprites
    public int _WallObjectIndex = 0;                // Which wall is currently showing

    [Export] private int _DoorIndex = -1;

    public override void _Ready()
    {
        base._Ready();
        
        // Store all objects in the wall list
        _WallObjects.Add(GetNode<Sprite2D>("Straight"));
        _WallObjects.Add(GetNode<Sprite2D>("Side"));
        _WallObjects.Add(GetNode<Sprite2D>("BottomLeft"));
        _WallObjects.Add( GetNode<Sprite2D>("BottomRight"));
        _WallObjects.Add(GetNode<Sprite2D>("TopLeft"));
        _WallObjects.Add(GetNode<Sprite2D>("TopRight"));
    }

    /// <summary>
    ///  Sets the wall we want to show manually
    /// </summary>
    /// <param name="wallName"></param>
    public void ShowWall(string wallName)
    {
        HideAllWalls();
        GetNode<Sprite2D>(wallName).Visible = true;
    }

    /// <summary>
    /// Gets the next wall
    /// </summary>
    public void CycleWallForward()
    {
        HideAllWalls();                         // Hide all the walls
        _WallObjectIndex += 1;                      // Set the new index
        
        // Check if we need to reset the index
        if (_WallObjectIndex >= _WallObjects.Count)
            _WallObjectIndex = 1;

        _WallObjects[_WallObjectIndex].Visible = true;                  // Show the wall
    }

    /// <summary>
    /// Gets the previous wall
    /// </summary>
    public void CycleWallBackwards()
    {
        HideAllWalls();                 // Hide all the walls
        _WallObjectIndex -= 1;              // Set the new index
        
        // Check if we need to reset the index
        if (_WallObjectIndex < 0)
            _WallObjectIndex = _WallObjects.Count - 1;

        _WallObjects[_WallObjectIndex].Visible = true;              // Display the wall
    }

    private void HideAllWalls()
    {
        foreach (var wall in _WallObjects)
            wall.Visible = false;
    }
}
