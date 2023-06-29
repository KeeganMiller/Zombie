using System;
using System.Collections.Generic;
using Godot;

public partial class PathController : Node2D
{
    private List<Vector2> _PathPoints = new List<Vector2>();                    // Reference to the points
    public int GetPathCount => _PathPoints.Count;                               // Reference to the amount of points in the path

    [Export] private bool _Circle;                  // If we circle the path, or move back and forth

    public override void _Ready()
    {
        base._Ready();
        // Get all the children nodes and add them to the list
        foreach (var c in this.GetChildren())
        {
            if(c is Node2D child)
                _PathPoints.Add(child.GlobalPosition);
        }
    }
    
    /// <summary>
    /// Returns the position to the next path point
    /// </summary>
    /// <param name="index">Index of the path point</param>
    /// <returns></returns>
    public Vector2 GetNextPathPoint(int index)
    {
        if (index < _PathPoints.Count)
        {
            return _PathPoints[index];
        }

        return Vector2.Zero;
    }
    
    
}