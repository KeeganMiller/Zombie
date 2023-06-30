using System;
using System.Collections.Generic;
using Godot;

public class SettlementController : Node2D
{
    private List<SettlerController> _Settlers = new List<SettlerController>();              // Reference to the settlers currently in settlement
    private int _MaxSettlers = 6;               // How many settlers 
    private List<BuildingController> _Buildings = new List<BuildingController>();               // Reference all the buildings in the settlement
}