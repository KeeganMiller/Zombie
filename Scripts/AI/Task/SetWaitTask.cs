using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;

public class SetWaitTask : Task
{
    private float _WaitTime = 5.0f;
    public SetWaitTask(BehaviorTree tree, float waitTime) : base(tree)
    {
        _WaitTime = waitTime;
    }

    public SetWaitTask(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            bb.SetValueAsBool("IsWaiting", true);
            bb.SetValueAsFloat("WaitTime", _WaitTime);
            return ETaskState.SUCCESS;
        }

        return ETaskState.FAILURE;

    }
}