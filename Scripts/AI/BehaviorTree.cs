using System;
using System.Collections.Generic;
using Godot;

public abstract class BehaviorTree
{
    protected Blackboard _BlackboardRef;
    public Blackboard BlackboardRef => _BlackboardRef;

    protected BaseCharacterController _Owner;
    public BaseCharacterController Owner => _Owner;

    public bool Paused = false;
    protected Task _RootTask;

    public BehaviorTree(Blackboard blackboard, BaseCharacterController owner)
    {
        _Owner = owner;
        _BlackboardRef = blackboard;
        _RootTask = CreateTree();
    }

    public void Update(float delta)
    {
        if (Paused || _BlackboardRef == null)
            return;

        if (_RootTask != null)
            _RootTask.RunTask(delta);
    }

    protected abstract Task CreateTree();
}