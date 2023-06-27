using System;
using System.Collections.Generic;
using Godot;

public class MoveToLocationCheck : Task
{
    public MoveToLocationCheck(BehaviorTree tree) : base(tree)
    {
    }

    public MoveToLocationCheck(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            if (bb.GetValueAsBool("HasMoveToLocation") && bb.GetValueAsVector2("MoveToLocation") != Vector2.Zero)
                return ETaskState.RUNNING;
        }

        return ETaskState.FAILURE;
    }
}