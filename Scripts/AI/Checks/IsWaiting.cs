using System.Collections.Generic;
using System;
using Godot;

public class IsWaiting : Task
{
    public IsWaiting(BehaviorTree tree) : base(tree)
    {
    }

    public IsWaiting(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            if (bb.GetValueAsBool("IsWaiting"))
                return ETaskState.SUCCESS;
        }

        return ETaskState.FAILURE;
    }
}