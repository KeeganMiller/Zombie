using System;
using System.Collections.Generic;
using Godot;

public class MoveToLocationTask : Task
{
    private const float STOPPING_DISTANCE = 0.5f;
    public MoveToLocationTask(BehaviorTree tree) : base(tree)
    {
    }

    public MoveToLocationTask(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            Node2D Self = bb.GetValueAsNode("Self");
            if (Self != null)
            {
                
            }
        }

        return ETaskState.FAILURE;
    }
}