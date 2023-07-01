using Godot;
using System.Collections.Generic;
using System.Diagnostics;


public partial class GridController : Node2D
{
    private GridNode[,] _Grid;
    public GridNode[,] Grid => _Grid;
    
    // === GRID PROPERTIES === //
    [ExportGroup("Grid Properties")]
    [Export] private int _GridSizeX;
    [Export] private int _GridSizeY;
    [Export] private float _CellSize;

    public int GridSizeX => _GridSizeX;
    public int GridSizeY => _GridSizeY;
    public float CellSize => _CellSize;
    
    // === TERRAIN SPAWNING === //
    private TerrainDatabase _TerrainDB;
    
    
    // === DEBUG SETTINGS === //
    [ExportGroup("Debug")]
    [Export] private bool _ShowGrid = true;
    [Export] private Color _GridColor;
    [Export] private PackedScene _DebugWall;

    public override void _Ready()
    {
        base._Ready();
        _TerrainDB = GetNode<TerrainDatabase>("/root/Ground");
        CreateGrid();
        GetNode<GameController>("/root/GameController").Grid = this;
        GetNode<AStar>("/root/AStar").Initialize(this);
        GetNode<SettlementController>("/root/SettlementController").IsInGame = true;
        GetNode<BuildModeController>("/root/BuildModeController").SetPlacingObject(_DebugWall);
        
        //GenerateGround();
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

    public GridNode GetGridNode(int x, int y) => _Grid[x, y];
    
    public override void _Process(double delta)
    {
        base._Process(delta);

    }

    private void GenerateGround()
    {
        // Validate the terrain database
        if (_TerrainDB != null)
        {
            RandomNumberGenerator rand = new RandomNumberGenerator();
            rand.Randomize();

            int initialTile = rand.RandiRange(0, 1);
            string tile = initialTile == 0 ? "Grass" : "Dirt";
            Sprite2D initialSprite = _TerrainDB.GetSprite(tile);
            _Grid[0, 0].SetGroundTile(tile, initialSprite);
            initialSprite.Position = _Grid[0, 0].CellPosition;
            this.AddChild(initialSprite);
            

            for (int x = 0; x < _Grid.GetLength(0); ++x)
            {
                for (int y = 0; y < _Grid.GetLength(1); ++y)
                {
                    if (x == 0 && y == 0)
                        continue;

                    string t = DetermineTile(x, y);
                    Sprite2D sprite = _TerrainDB.GetSprite(t);
                    if (sprite != null)
                    {
                        sprite.Position = _Grid[x, y].CellPosition;
                        this.AddChild(sprite);
                        _Grid[x, y].SetGroundTile(t, sprite);
                    }

                }
            }
        }
    }

    private string DetermineTile(int x, int y)
    {
        int grassCount = 0;
        int dirtCount = 0;
        int grassToDirt = 0;
        int DirtToGrass = 0;
        
        for(int dx = -1; dx <= 1; ++dx)
        {
            for (int dy = -1; dy <= 1; ++dy)
            {
                if (dx == 0 && dy == 0)
                    continue;

                int neighborX = x + dx;
                int neighborY = y + dy;

                if (neighborX < 0 || neighborX >= _Grid.GetLength(0))
                    continue;
                if (neighborY < 0 || neighborY >= _Grid.GetLength(1))
                    continue;

                if (_Grid[neighborX, neighborY].GroundTileName == "Grass")
                    grassCount += 1;
                else if (_Grid[neighborX, neighborY].GroundTileName == "Dirt")
                    dirtCount += 1;
                else if (_Grid[neighborX, neighborY].GroundTileName == "DirtToGrass")
                    DirtToGrass += 1;
                else if (_Grid[neighborX, neighborY].GroundTileName == "GrassToDirt")
                    grassToDirt += 1;
            }
        }

        return "";
    }

    public override void _Draw()
    {
        base._Ready();
        if (!_ShowGrid)
            return;
        
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
                if (position.X >= nodePosition.X && position.X < (nodePosition.X + CellSize))
                {
                    if (position.Y >= nodePosition.Y && position.Y < (nodePosition.Y + CellSize))
                    {
                        return _Grid[x, y];
                    }
                }
            }
        }

        return null;
    }
}