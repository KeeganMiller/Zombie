using Godot;
using System;
using System.Collections.Generic;

public class GridNode
{
	private GridController _GridRef;					// Reference to the grid
	private Vector2 _CellPosition;						// Reference to the cell position
	public Vector2 CellPosition => _CellPosition;
	private int _CellIndexX;							// Reference to the cell X index
	public int CellIndexX => _CellIndexX;
	private int _CellIndexY;							// Reference to the cell Y index
	public int CellIndexY => _CellIndexY;
	
	
	
	// === PATHFINDING === //
	public GridNode Parent;
	public bool IsWalkable = true;					// If this node can be walked on
	public int GCost;
	public int HCost;
	public int FCost => GCost + HCost;
	public List<GridNode> _Neighbors = new List<GridNode>();
	
	public GridNode(GridController grid, Vector2 gridPosition, int cellX, int cellY)
	{
		_GridRef = grid;
		_CellPosition = gridPosition;
		_CellIndexX = cellX;
		_CellIndexY = cellY;
	}
}
