using System;
using System.Collections.Generic;
using Godot;

public partial class SettlementController : Node2D
{
    private List<SettlerController> _Settlers = new List<SettlerController>();              // Reference to the settlers currently in settlement
    private int _MaxSettlers = 6;               // How many settlers 
    private List<BuildingController> _Buildings = new List<BuildingController>();               // Reference all the buildings in the settlement

    private SettlementResources _Resources;
    public SettlementResources Resources => _Resources;

    private Timer _ResourceReductionTimer;

    public bool IsInGame = false;

    public override void _Ready()
    {
        base._Ready();
        _Resources = new SettlementResources();
        _ResourceReductionTimer = new Timer("ResourceReductionTimer", 1.0f, true);
        _ResourceReductionTimer.AddAction(OnResourceTimerComplete);
    }

    public override void _Process(double delta)
    {
        if (!IsInGame)
            return;
        
        base._Process(delta);
        
        if(_ResourceReductionTimer != null)
            _ResourceReductionTimer.Update((float)delta);
        
        
    }

    /// <summary>
    /// Adds a new settler to the settlement
    /// </summary>
    /// <param name="settler">Settler to add</param>
    /// <returns>If the settler was added or not</returns>
    public bool AddSettler(SettlerController settler)
    {
        if (_Settlers.Count >= _MaxSettlers || _Settlers.Contains(settler))
            return false;
        
        _Settlers.Add(settler);
        _Resources.IncreaseMinFood(settler.FoodPerSecond);
        _Resources.IncreaseMinWater(settler.WaterPerSecond);
        return true;
    }

    /// <summary>
    /// Removes a settler from the settlement
    /// </summary>
    /// <param name="settler">Settler to remove</param>
    /// <returns>If the settler was removed or not</returns>
    public bool RemoveSettler(SettlerController settler)
    {
        if (_Settlers.Contains(settler))
        {
            _Settlers.Remove(settler);
            _Resources.DecreaseMinFood(settler.FoodPerSecond);
            _Resources.DecreaseMinWater(settler.WaterPerSecond);
            return true;
        }

        return false;
    }

    public void AddBuilding(BuildingController building)
    {
        if(_Buildings.Contains(building))
            _Buildings.Add(building);
    }

    public void RemoveBuilding(BuildingController building)
    {
        if (_Buildings.Contains(building))
            _Buildings.Remove(building);
    }

    private void OnResourceTimerComplete()
    {
        float foodReduction = 0f;
        float waterReduction = 0f;
        foreach (var settler in _Settlers)
        {
            foodReduction += settler.FoodPerSecond;
            waterReduction += settler.WaterPerSecond;
        }
        
        _Resources.ReduceFood(foodReduction);
        _Resources.ReduceWater(waterReduction);
    }
}