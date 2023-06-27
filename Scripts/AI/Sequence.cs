using System;
using System.Collections.Generic;
using Godot;

public class Sequence : Task
{
    public Sequence(BehaviorTree tree) : base(tree)
    {
    }

    public Sequence(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        foreach (var child in _Children)
        {
            switch (child.RunTask(delta))
            {
                case ETaskState.FAILURE:
                    return ETaskState.FAILURE;
                case ETaskState.RUNNING:
                    return ETaskState.RUNNING;
                case ETaskState.SUCCESS:
                    continue;
            }
        }

        return ETaskState.FAILURE;
    }
}