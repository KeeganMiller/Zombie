using Godot;
using System.Collections.Generic;


public partial class GridController : Node2D
{
    private GridNode[,] _Grid;
    
    // === GRID PROPERTIES === //
    [Export] private int _GridSizeX;
    [Export] private int _GridSizeY;
    [Export] private float _CellSize;
    
    // === DEBUG SETTINGS === //
    [Export] private bool _ShowGrid = true;
    [Export] private Color _GridColor;
    
    public override void _Ready()
    {
        base._Ready();
        CreateGrid();
    }

    private void CreateGrid()
    {
        // Create the grid
        _Grid = new GridNode[_GridSizeX, _GridSizeY];
        
        // Get the position starting point
        float startPointX = this.Position.X;
        float startPointY = this.Position.Y;
        
        // Create positional vector
        Vector2 currentPos = new Vector2(startPointX, startPointY);

        for (int y = 0; y < _Grid.GetLength(1); ++y)
        {
            for (int x = 0; x < _Grid.GetLength(0); ++x)
            {
                _Grid[x, y] = new GridNode(this, currentPos, x, y);             // Create a new grid node
                float posX = currentPos.X + _CellSize;                  // Get the next x position
                currentPos = new Vector2(posX, currentPos.Y);           // Set the next position
            }

            float posY = currentPos.Y + _CellSize;                          // Get the next Y position
            // Set the current position, resetting start poiint
            currentPos = new Vector2(startPointX, posY);
        }
        
        if(_ShowGrid)
            QueueRedraw();
        
    }

    public override void _Draw()
    {
        base._Ready();
        if (_Grid != null)
        {
            for (int y = 0; y < _Grid.GetLength(1); ++y)
            {
                for (int x = 0; x < _Grid.GetLength(0); ++x)
                {
                    Vector2 startPos = _Grid[x, y].CellPosition;
                    Vector2 endPosX = new Vector2((startPos.X - _CellSize), startPos.Y);
                    Vector2 endPosY = new Vector2(startPos.X, (startPos.Y - _CellSize));
                    
                    DrawLine(startPos, endPosX, _GridColor, 2f);
                    DrawLine(startPos, endPosY, _GridColor, 2f);
                }
            }
        }
    }
}