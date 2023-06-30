using Godot;
using System;
using System.Collections.Generic;

public class SettlerController : BaseCharacterController
{
    [ExportGroup("Resource Usage")] 
    [Export] private float _FoodPerSecond;
    private float _FoodModifier;                    // How much they save in food
    [Export] private float _WaterPerSecond;
    private float _WaterModifier;                   // How water they save

    public float FoodPerSecond
    {
        get {
            return _FoodPerSecond * _FoodModifier;
        }
    }

    public float WaterPerSecond
    {
        get {
            return _WaterPerSecond * _WaterModifier;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        _BTree = new SettlerTree(_Blackboard, this);
    }
    
    protected override void CreateBlackboard()
    {
        base.CreateBlackboard();
        _Blackboard.SetValueAsInt("MovementState", (int)EMovementState.FOLLOW_PATH);
        _Blackboard.SetValueAsInt("CurrentPathIndex", 0);
        _Blackboard.SetValueAsBool("HasPathPoint", false);
        _Blackboard.SetValueAsBool("IsWaiting", false);
        _Blackboard.SetValueAsFloat("WaitTime", 5.0f);
    }
}