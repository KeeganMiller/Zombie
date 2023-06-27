using System;
using System.Collections.Generic;
using Godot;

public class Selector : Task
{
    public Selector(BehaviorTree tree) : base(tree)
    {
    }

    public Selector(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        foreach (var child in _Children)
        {
            switch (child.RunTask(delta))
            {
                case ETaskState.FAILURE:
                    continue;
                case ETaskState.RUNNING:
                    return ETaskState.RUNNING;
                case ETaskState.SUCCESS:
                    return ETaskState.SUCCESS;
            }
        }

        return ETaskState.FAILURE;
    }
}