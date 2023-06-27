using System;
using System.Collections.Generic;
using Godot;

public class BehaviorTree
{
    protected Blackboard _BlackboardRef;
    public Blackboard BlackboardRef => _BlackboardRef;

    public bool Paused = false;

    public BehaviorTree(Blackboard blackboard)
    {
        _BlackboardRef = blackboard;
    }

    public void Update(float delta)
    {
        if (Paused || _BlackboardRef == null)
            return;
    }
}