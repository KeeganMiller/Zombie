using System;
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
    [ExportGroup("Building Size")]
    [Export] private int _BuildingSizeX;
    [Export] private int _BuildingSizeY;
    [Export] private int _DoorIndex;

    public int BuildingSizeX => _BuildingSizeX;
    public int BuildingSizeY => _BuildingSizeY;
    public int DoorIndex => _DoorIndex;

    [ExportGroup("Power Settings")] 
    [Export] private bool _RequiresPower;                   // If this building requires power or not
    [Export] private float _BasePowerUsage;               // How much this building uses in power per second
    [Export] private float _PowerSavingModifier = 1.0f;               // Adjust how much power is saved on this room

    [ExportGroup("Resource Generation")] 
    [Export] private bool _GeneratesResource = true;
    [Export] private EResourceType _ResourceType = EResourceType.OTHER;



}