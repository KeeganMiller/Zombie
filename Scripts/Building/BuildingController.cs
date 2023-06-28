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
    [Export] protected string _BuildingName;
    [Export] protected EBuildingType _BuildingType = EBuildingType.OTHER;

    public string BuildingName => _BuildingName;
    public EBuildingType BuildingType => _BuildingType;
    
    // === BUILDING SIZE === //
    [ExportGroup("Building Size")]
    [Export] protected int _BuildingCellSizeX;
    [Export] protected int _BuildingCellSizeY;

    public int BuildingSizeX => _BuildingCellSizeX;
    public int BuildingSizeY => _BuildingCellSizeY;

    [ExportGroup("Power Settings")]
    [Export] private float _PowerUsage;
    [Export(PropertyHint.Range, "0, 1")] protected float _PowerSavingModifier = 1.0f;

    [ExportGroup("Unit Generation")] 
    [Export] protected bool _GeneratesUnits = true;
    [Export] protected float _UnitsPerSecond;
    [Export] protected float _BaseRoomSpeed;
    [Export(PropertyHint.Range, "1, 2")] protected float _SpeedModifier = 1.0f;
    protected bool _UnitsReady = false;

    protected Timer _ProductionTimer;

    [ExportGroup("Node Components")]
    [Export] private NodePath _ProductionReadySprite;

    

    public override void _Ready()
    {
        _ProductionTimer = new Timer($"{_BuildingName}_Production", _BaseRoomSpeed);
        if (_ProductionTimer != null)
        {
            _ProductionTimer.AddAction(OnProductionComplete);
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(_ProductionTimer != null)
            _ProductionTimer.Update((float)delta);
    }

    protected virtual void OnProductionComplete()
    {
        _UnitsReady = true;
    }

    protected virtual void OnCollectProduction()
    {
        _UnitsReady = false;
    }
}