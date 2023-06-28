using System;
using System.Collections.Generic;
using Godot;


public partial class BuildingController : Node2D
{
    [Export] protected bool _IsWalkable = false;
    public bool IsWalkable = true;
}