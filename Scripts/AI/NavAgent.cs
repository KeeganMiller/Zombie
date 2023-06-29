using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class NavAgent : Node2D
{
    // === NODES === //
    private BaseCharacterController _Owner;
    private AStar _Pathfinding;
    
    // === Path Finding Settings === //
    [Export] public float StoppingDistance = 0.5f;              // Distance from the final path that we are stopping at
    [Export] public float PathPointStoppingDistance = 0.3f;
    private List<GridNode> _Path = new List<GridNode>();                    // List of the current path to follow
    public bool HasPath = false;                            // If the agent has a path
    public bool HasReachPath = false;                       // If the agent has reached the final point in the path
    private Vector2 _FinalPosition;                             // Final position the agent is to move to
    
    // === CURRENT PATH SETTINGS === //
    private GridNode _CurrentNode;                          // Reference to the node we are currently on
    private GridNode _NextNode;                             // Reference to the next node we are moving to
    
    // === MISC === //
    private GridController _Grid;

    public event Action _PathComplete; 
    public override void _Ready()
    {
        base._Ready();
        
        _Owner = this.GetParent<BaseCharacterController>();
        _Pathfinding = GetNode<AStar>("/root/AStar");
        
        
        Callable.From(ActorSetup).CallDeferred();
    }

    private void UpdatePathNodes()
    {
        if (_Path.Count > 0 && _Grid != null)
        {
            _CurrentNode = _Grid.GetNodeFromPosition(this.GlobalPosition);
            if (_CurrentNode == _Path[0] && _Path.Count > 1)
            {
                _NextNode = _Path[1];
                GD.Print($"{_Path.Count}");
            }
            else
            {
                _Path.Insert(0, _CurrentNode);
                _NextNode = _Path[1];
            }

            float distanceTo = this.GlobalPosition.DistanceTo(_NextNode.CellPosition);
            GD.Print(distanceTo);
            if (distanceTo < PathPointStoppingDistance)
            {
                if (_Path.Count > 1)
                {
                    _Path.RemoveAt(0);
                    _CurrentNode = _Path[0];
                    if (_Path.Count > 1)
                        _NextNode = _Path[1];
                }
            }
            
        }
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
            UpdatePathNodes();
        }
        else
        {  
            GD.Print("Cannot determine path");
            HasPath = false;
            HasReachPath = false;
        }

        
    }

    public void Stop()
    {
        HasPath = false;
        HasReachPath = true;
    }

    private async void ActorSetup()
    {
        await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);
        
        _Grid = GetNode<GameController>("/root/GameController").Grid;

        
    }

    public Vector2 GetMovementDirection()
    {
        var velocity = Vector2.Zero;
        UpdatePathNodes();

        if (!HasPath)
            return velocity;


        if (_CurrentNode != null && _NextNode != null)
        {
            if (_NextNode.CellPosition.X > _CurrentNode.CellPosition.X)
                return new Vector2(1, 0);
            if (_NextNode.CellPosition.X < _CurrentNode.CellPosition.X)
                return new Vector2(-1, 0);
            if (_NextNode.CellPosition.Y > _CurrentNode.CellPosition.Y)
                return new Vector2(0, 1);
            if (_NextNode.CellPosition.Y < _CurrentNode.CellPosition.Y)
                return new Vector2(0, -1);
        }
        else
        {
            HasReachPath = true;
            HasPath = false;
        }

        return velocity;
    }
    
}