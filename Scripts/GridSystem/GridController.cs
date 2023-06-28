using Godot;
using System.Collections.Generic;
using System.Diagnostics;


public partial class GridController : Node2D
{
    private GridNode[,] _Grid;
    public GridNode[,] Grid => _Grid;
    
    // === GRID PROPERTIES === //
    [Export] private int _GridSizeX;
    [Export] private int _GridSizeY;
    [Export] private float _CellSize;

    public int GridSizeX => _GridSizeX;
    public int GridSizeY => _GridSizeY;
    public float CellSize => _CellSize;
    
    // === TERRAIN SPAWNING === //
    private TerrainDatabase _TerrainDB;
    
    
    // === DEBUG SETTINGS === //
    [Export] private bool _ShowGrid = true;
    [Export] private Color _GridColor;
    
    public override void _Ready()
    {
        base._Ready();
        _TerrainDB = GetNode<TerrainDatabase>("/root/Ground");
        CreateGrid();
        GetNode<GameController>("/root/GameController").Grid = this;
        GetNode<AStar>("/root/AStar").Initialize(this);
        
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
                
                // Validate the terrain database
                if (_TerrainDB != null)
                {
                    Sprite2D sprite = _TerrainDB.GetSprite("Ground_1");             // Get reference to a sprite
                    // Validate the sprite and add to the world
                    if (sprite != null)
                    {
                        sprite.Position = currentPos;
                        this.AddChild(sprite);
                    }
                }
                
                // Update position
                float posX = currentPos.X + _CellSize;                  // Get the next x position
                currentPos = new Vector2(posX, currentPos.Y);           // Set the next position
            }

            float posY = currentPos.Y + _CellSize;                          // Get the next Y position
            // Set the current position, resetting start poiint
            currentPos = new Vector2(startPointX, posY);
        }

        for (int i = 0; i < _Grid.GetLength(1); ++i)
            for(int j = 0; j < _Grid.GetLength(0); ++j)
                AddNeighbors(_Grid[j, i]);

        if(_ShowGrid)
            QueueRedraw();
        
    }

    private void AddNeighbors(GridNode node)
    {
        int x = node.CellIndexX;
        int y = node.CellIndexY;
        
        if(x > 0)
            node._Neighbors.Add(_Grid[x - 1, y]);
        if(x < (GridSizeX - 1))
            node._Neighbors.Add(_Grid[x + 1, y]);
        if(y > 0)
            node._Neighbors.Add(_Grid[x, (y - 1)]);
        if(y < (_GridSizeY - 1))
            node._Neighbors.Add(_Grid[x, y + 1]);
    }
    
    public override void _Process(double delta)
    {
        base._Process(delta);

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

    public GridNode GetNodeFromPosition(Vector2 position)
    {
        for(int y = 0; y < _Grid.GetLength(1); ++y)
        {
            for (int x = 0; x < _Grid.GetLength(1); ++x)
            {
                Vector2 nodePosition = _Grid[x, y].CellPosition;
                if (position.X > nodePosition.X && position.X < (nodePosition.X + CellSize))
                {
                    if (position.Y > nodePosition.Y && position.Y < (nodePosition.Y + CellSize))
                    {
                        return _Grid[x, y];
                    }
                }
            }
        }

        return null;
    }
}