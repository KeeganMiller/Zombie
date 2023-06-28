using System;
using System.Collections.Generic;
using Godot;

public class Timer
{
    public string TimerName = "";
    public float TimerLength;
    private float _CurrentTime;
    public bool _IsActive;
    private bool _Loop;
    private event Action _TimerCompleteActions;

    public Timer(string name, float length, bool loop = false)
    {
        TimerName = name;
        TimerLength = length;
        _IsActive = true;
        _Loop = loop;
    }
    
    

    public void AddAction(Action e) => _TimerCompleteActions += e;
}