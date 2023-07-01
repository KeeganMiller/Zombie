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
    [ExportGroup("Building Details")] 
    [Export] private string _BuildingName;

    public string BuildingName => _BuildingName;
    
    [ExportGroup("Building Size")]
    [Export] protected int _BuildingSizeX;
    [Export] protected int _BuildingSizeY;
    [Export] protected int _DoorIndex;
    [Export] protected Vector2[] _NonWalkableTiles;

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
    
    // === DOOR === //
    [ExportGroup("Door Settings")]
    [Export] private NodePath _AnimPlayerPath;
    private AnimationPlayer _AnimPlayer;
    private bool _IsOpen = false;

    public override void _Ready()
    {
        base._Ready();

        _AnimPlayer = GetNode<AnimationPlayer>(_AnimPlayerPath);

        _ResourceTimer = new Timer($"{_BuildingName} - ResourceTimer", (_BaseResourceWaitTime * _ResourceSpeedModifier),
            false);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        // Update the resource timer
        if(_ResourceTimer != null)
            _ResourceTimer.Update((float)delta);
        
        if(Input.IsActionPressed("MoveUp"))
            OnCharacterEnter();
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

    public void OnCharacterEnter()
    {
        if (_AnimPlayer == null)
            return;

        if (_IsOpen)
        {
            _IsOpen = false;
            _AnimPlayer.PlayBackwards("DoorAnimation");
        }
        else
        {
            _IsOpen = true;
            _AnimPlayer.Play("DoorAnimation");
        }
    }

    public bool _IsCellWalkable(int x, int y)
    {
        foreach (var cell in _NonWalkableTiles)
        {
            if (cell.X == x && cell.Y == y)
                return false;
        }

        return true;
    }


}