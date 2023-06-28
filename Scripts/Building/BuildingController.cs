using System;
using System.Collections.Generic;
using Godot;

public enum EBuildingType
{
    POWER,
    FOOD,
    WATER,
    OTHER
}

public partial class BuildingController : Node2D
{
    // === BUILDING DETAILS === //
    [ExportGroup("Building Details")]
    [Export] private string _BuildingName;
    [Export] private EBuildingType _BuildingType = EBuildingType.OTHER;

    public string BuildingName => _BuildingName;
    public EBuildingType BuildingType => _BuildingType;
    
    // === BUILDING SIZE === //
    [ExportGroup("Building Size")]
    [Export] private int _BuildingCellSizeX;
    [Export] private int _BuildingCellSizeY;

    public int BuildingSizeX => _BuildingCellSizeX;
    public int BuildingSizeY => _BuildingCellSizeY;

    [ExportGroup("Power Settings")]
    [Export] private float _PowerUsage;
    [Export(PropertyHint.Range, "0, 1")] private float _PowerSavingModifier = 1.0f;

    [ExportGroup("Unit Generation")] 
    [Export] private bool _GeneratesUnits = true;
    [Export] private float _UnitsPerSecond;
    [Export(PropertyHint.Range, "1, 2")] private float _SpeedModifier = 1.0f;

}