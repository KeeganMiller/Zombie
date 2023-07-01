﻿using System;
using System.Collections.Generic;
using Godot;

public partial class DebugController : Node2D
{
    [Export] private NodePath _DebugSettler;
    private SettlerController settler;
    public override void _Ready()
    {
        base._Ready();
        settler = GetNode<SettlerController>(_DebugSettler);
        if (settler != null)
            GetNode<SettlementController>("/root/SettlementController").AddSettler(settler);
    }

    public override void _Process(double delta)
    {
        if (!GetNode<BuildModeController>("/root/BuildModeController").IsBuildMode)
        {
            if (Input.IsActionJustPressed("LeftMouseClicked"))
            {
                settler.GetBlackboard().SetValueAsBool("HasMoveToLocation", true);
                settler.GetBlackboard().SetValueAsVector2("MoveToLocation", GetGlobalMousePosition());
            }
        }
    }
}