using System;
using System.Collections.Generic;
using Godot;

public abstract class BehaviorTree
{
    protected Blackboard _BlackboardRef;
    public Blackboard BlackboardRef => _BlackboardRef;

    public bool Paused = false;
    protected Task _RootTask;

    public BehaviorTree(Blackboard blackboard)
    {
        _BlackboardRef = blackboard;
        _RootTask = CreateTree();
    }

    public void Update(float delta)
    {
        if (Paused || _BlackboardRef == null)
            return;
    }

    protected abstract Task CreateTree();
}