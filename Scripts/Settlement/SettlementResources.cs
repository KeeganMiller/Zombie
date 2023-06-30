using System;
using System.Collections.Generic;
using System.Threading;
using Godot;

public class SettlementResources
{
    public bool IsActive = false;
    #region Current Resources
    
    private float _CurrentFood;
    private float _CurrentWater;
    private float _CurrentPower;

    public int CurrentFood => Mathf.FloorToInt(_CurrentFood);
    public int CurrentWater => Mathf.FloorToInt(_CurrentWater);
    public int CurrentPower => Mathf.FloorToInt(_CurrentPower);
    
    #endregion

    #region Minimum Resources
    
    private float _MinFood;
    private float _MinWater;
    private float _MinPower;

    public int MinFood => Mathf.FloorToInt(_MinFood);
    public int MinWater => Mathf.FloorToInt(_MinWater);
    public int MinPower => Mathf.FloorToInt(_MinPower);
    
    #endregion
    
    #region Maximum Resources

    private float _MaxFood;
    private float _MaxWater;
    private float _MaxPower;

    public int MaxFood => Mathf.FloorToInt(_MaxFood);
    public int MaxWater => Mathf.FloorToInt(_MaxWater);
    public int MaxPower => Mathf.FloorToInt(_MaxPower);

    #endregion

    public void Update(float dt)
    {
        if (!IsActive)
            return;
        
        
        
    }

    public void ReduceFood(float amt)
    {
        _CurrentFood -= amt;
        if (_CurrentFood < 0f)
            _CurrentFood = 0f;
    }

    public void ReduceWater(float amt)
    {
        _CurrentWater -= amt;
        if (_CurrentWater < 0f)
            _CurrentWater = 0f;
    }

    public void ReducePower(float amt)
    {
        _CurrentPower -= amt;
        if (_CurrentPower < 0f)
            _CurrentPower = 0f;
    }

}