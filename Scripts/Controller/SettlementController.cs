using System;
using System.Collections.Generic;
using Godot;

public class SettlementController : Node2D
{
    private List<SettlerController> _Settlers = new List<SettlerController>();              // Reference to the settlers currently in settlement
    private int _MaxSettlers = 6;               // How many settlers 
    private List<BuildingController> _Buildings = new List<BuildingController>();               // Reference all the buildings in the settlement


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
            return true;
        }

        return false;
    }
}