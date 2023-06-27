using Godot;
using System;

public class GridNode
{
	private GridController _GridRef;					// Reference to the grid
	private Vector2 _CellPosition;
	public Vector2 CellPosition => _CellPosition;
	private int _CellIndexX;
	private int _CellIndexY;
	public GridNode(GridController grid, Vector2 gridPosition, int cellX, int cellY)
	{
		_GridRef = grid;
		_CellPosition = gridPosition;
		_CellIndexX = cellX;
		_CellIndexY = cellY;
	}
}
