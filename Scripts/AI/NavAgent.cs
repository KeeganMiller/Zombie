using Godot;
using System;
using System.Collections.Generic;

public partial class NavAgent : Node2D
{
    // === NODES === //
    private BaseCharacterController _Owner;
    private AStar _Pathfinding;
    
    // === Path Finding Settings === //
    [Export] public float StoppingDistance = 0.5f;              // Distance from the final path that we are stopping at
    private List<GridNode> _Path = new List<GridNode>();                    // List of the current path to follow
    public bool HasPath = false;                            // If the agent has a path
    public bool HasReachPath = false;                       // If the agent has reached the final point in the path
    private Vector2 _FinalPosition;                             // Final position the agent is to move to

    public event Action _PathComplete; 
    public override void _Ready()
    {
        base._Ready();
        
        _Owner = this.GetParent<BaseCharacterController>();
        _Pathfinding = GetNode<AStar>("/root/AStar");
        
        Callable.From(ActorSetup).CallDeferred();
    }

    public Vector2 GetNextPathPoint()
    {
        if (HasReachPath)
            return Vector2.Zero;

        // Get reference to the grid controller, if we can't than zero out the movement
        GridController grid = GetNode<GameController>("/root/GameController")?.Grid;            
        if (grid == null)
            return Vector2.Zero;

        Vector2 currentPos = this.GlobalPosition;                   // Get the current position of the agent
        GridNode currentNode = grid.GetNodeFromPosition(currentPos);            // Get the current node of the agent
        // If the current node is not the last, than remove the current node
        if (currentNode == _Path[0])
            _Path.RemoveAt(0);
        // If we still have a path, than get that cell position
        if (_Path.Count > 0)
        {
            return _Path[0].CellPosition;
        }
        else
        {
            // Get the distance to the final point
            float distanceToFinal = currentPos.DistanceTo(_FinalPosition);
            // If within distance than complete the path
            if (distanceToFinal >= StoppingDistance)
            {
                HasReachPath = true;
                HasPath = false;
                _FinalPosition = Vector2.Zero;
                _PathComplete?.Invoke();
            }
            return _FinalPosition;
        }

        return Vector2.Zero;
    }

    /// <summary>
    /// Sets the target location navigate to and calculates the path
    /// </summary>
    /// <param name="target">Target Destination</param>
    public void SetTargetLocation(Vector2 target)
    {
        if (_Pathfinding == null)
        {
            GD.PrintErr("#NavAgent::SetTargetLocation - AStar not referenced");
            return;
        }

        List<GridNode> path = _Pathfinding.FindPath(this.GlobalPosition, target);
        if (path != null)
        {
            _Path = path;
            HasPath = true;
            HasReachPath = false;
            _FinalPosition = target;
        }
        else
        {  
            GD.Print("Cannot determine path");
            HasPath = false;
            HasReachPath = false;
        }

        
    }

    private async void ActorSetup()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

        
    }
    
}