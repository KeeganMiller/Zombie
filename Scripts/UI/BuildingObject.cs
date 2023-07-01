using Godot;
using System;
using System.Diagnostics;

public partial class BuildingObject : Button
{
	[Export] private PackedScene _BuildingObject;				// Reference to the building object
	[Export] public int Cost;					// How the building object cost

	public void OnClickObject()
	{
		BuildModeController buildMode = GetNode<BuildModeController>("/root/BuildModeController");
		if (buildMode != null)
		{
			buildMode.SetPlacingObject(_BuildingObject, false);
		}
	}
}
