using System;
using System.Collections.Generic;
using Godot;

public class AStar
{
    public GridController _Grid;

    public AStar(GridController grid)
    {
        _Grid = grid;
        Initialize();
    }

    public void Initialize()
    {
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
}