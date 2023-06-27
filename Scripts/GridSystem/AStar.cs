﻿using System;
using System.Collections.Generic;
using Godot;

public partial class AStar : Node2D
{
    public GridController _Grid;
    

    public void Initialize(GridController grid)
    {
        _Grid = grid;
        for (int x = 0; x < _Grid.GridSizeX; ++x)
        {
            for (int y = 0; y < _Grid.GridSizeY; ++y)
            {
                AddNeighbors(_Grid.Grid[x, y]);
            }
        }
    }

    private void AddNeighbors(GridNode node)
    {
        GridNode[,] grid = _Grid.Grid;
        int x = node.CellIndexX;
        int y = node.CellIndexY;
        
        if(x > 0)
            node._Neighbors.Add(grid[x - 1, y]);
        if(x < (_Grid.GridSizeX - 1))
            node._Neighbors.Add(grid[x + 1, y]);
        if(y > 0)
            node._Neighbors.Add(grid[x, (y - 1)]);
        if(y < (_Grid.GridSizeX - 1))
            node._Neighbors.Add(grid[x, y + 1]);
    }

    public List<GridNode> FindPath(Vector2 startPoint, Vector2 endPoint)
    {
        // Validate the grid node
        if (_Grid == null)
            return new List<GridNode>();
        
        // Get the nodes from their position
        GridNode startNode = _Grid.GetNodeFromPosition(startPoint);
        GridNode endNode = _Grid.GetNodeFromPosition(endPoint);

        // Create open & closed sets to store nodes being process/have been process
        List<GridNode> openSet = new List<GridNode>();
        HashSet<GridNode> closedSet = new HashSet<GridNode>();
        
        openSet.Add(startNode);                 // Add the start node to initialize the path finding

        while (openSet.Count > 0)
        {
            GridNode current = openSet[0];
            for (int i = 1; i < openSet.Count; ++i)
            {
                if (openSet[i].FCost < current.FCost ||
                    openSet[i].FCost == current.FCost && openSet[i].HCost < current.HCost)
                {
                    current = openSet[i];
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == endNode)
                return GeneratePath(startNode, endNode);

            foreach (var neighbor in current._Neighbors)
            {
                if (!neighbor.IsWalkable || closedSet.Contains(neighbor))
                    continue;

                int newGCost = current.GCost + CalculateDistance(current, neighbor);
                if (newGCost < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newGCost;
                    neighbor.HCost = CalculateDistance(neighbor, endNode);
                    neighbor.Parent = current;
                    
                    if(!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    private List<GridNode> GeneratePath(GridNode startNode, GridNode endNode)
    {
        List<GridNode> path = new List<GridNode>();
        GridNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        
        path.Reverse();
        return path;
    }

    private int CalculateDistance(GridNode a, GridNode b)
    {
        int distanceX = Math.Abs(a.CellIndexX - b.CellIndexX);
        int distanceY = Math.Abs(a.CellIndexY - b.CellIndexY);
        return distanceX + distanceY;
    }
}