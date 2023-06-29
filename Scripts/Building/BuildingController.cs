﻿using System;
using System.Collections.Generic;
using Godot;

public enum EResourceType
{
    FOOD,
    WATER,
    POWER,
    OTHER
}

public partial class BuildingController : Node2D
{
    [ExportGroup("Building Details")] 
    [Export] private string _BuildingName;

    public string BuildingName => _BuildingName;
    
    [ExportGroup("Building Size")]
    [Export] protected int _BuildingSizeX;
    [Export] protected int _BuildingSizeY;
    [Export] protected int _DoorIndex;

    public int BuildingSizeX => _BuildingSizeX;
    public int BuildingSizeY => _BuildingSizeY;
    public int DoorIndex => _DoorIndex;

    [ExportGroup("Power Settings")] 
    [Export] protected bool _RequiresPower;                   // If this building requires power or not
    [Export] protected float _BasePowerUsage;               // How much this building uses in power per second
    [Export] protected float _PowerSavingModifier = 1.0f;               // Adjust how much power is saved on this room

    [ExportGroup("Resource Generation")] 
    [Export] protected bool _GeneratesResource = true;                    // If this building is generating resources
    [Export] protected EResourceType _ResourceType = EResourceType.OTHER;             // Type of resource being generated
    [Export] protected float _ResourcesPerCollection;                      // How many resourse is collected when the room is ready
    [Export] protected float _BaseResourceWaitTime;
    [Export] protected float _ResourceSpeedModifier = 1.0f;               // How much faster the timer is 
    private Timer _ResourceTimer;

    public override void _Ready()
    {
        base._Ready();

        _ResourceTimer = new Timer($"{_BuildingName} - ResourceTimer", (_BaseResourceWaitTime * _ResourceSpeedModifier),
            false);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        // Update the resource timer
        if(_ResourceTimer != null)
            _ResourceTimer.Update((float)delta);
    }

    protected virtual void OnResourceComplete()
    {
        
    }

    public virtual void AddSettler()
    {
        
    }

    public void OnCollectResource()
    {
        
    }


}