using System;
using System.Collections.Generic;
using Godot;

public class Timer
{
    public string TimerName = "";               // Reference to the name of the timer
    public float TimerLength;                   // How long the timer runs for
    private float _CurrentTime;                 // Current time of the timer
    public bool IsActive;                       // If we should update the timer
    private bool _Loop;                         // Will timer restart on loop
    private event Action _TimerCompleteActions;             // actions for when timer is completed

    public Timer(string name, float length, bool loop = false)
    {
        TimerName = name;
        TimerLength = length;
        IsActive = true;
        _Loop = loop;
    }

    public void Update(float delta)
    {
        // Make sure the timer is active
        if (!IsActive)
            return;

        _CurrentTime += 1 * delta;              // Increment the timer

        // If the timer is great than the length, timer is complete
        if (_CurrentTime > TimerLength)
            CompleteTimer();
    }

    private void CompleteTimer()
    {
        _TimerCompleteActions?.Invoke();
        _CurrentTime = 0f;
        IsActive = _Loop;
    }

    public void AddAction(Action e) => _TimerCompleteActions += e;
}