using System;
using System.Collections.Generic;
using Godot;

public partial class DebugController : Node2D
{
    [Export] private NodePath _DebugSettler;

    public override void _Ready()
    {
        base._Ready();
        SettlerController settler = GetNode<SettlerController>(_DebugSettler);
        if (settler != null)
            GetNode<SettlementController>("/root/SettlementController").AddSettler(settler);
    }
}