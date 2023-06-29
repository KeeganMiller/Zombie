using System.Collections.Generic;
using System;
using Godot;

public class WaitTask : Task
{
    private float _CurrentTime = 0.0f;
    
    public WaitTask(BehaviorTree tree) : base(tree)
    {
    }

    public WaitTask(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            float waitTime = bb.GetValueAsFloat("WaitTime");
            _CurrentTime += 1 * delta;

            if (_CurrentTime > waitTime)
            {
                bb.SetValueAsBool("IsWaiting", false);
                _CurrentTime = 0.0f;
                return ETaskState.SUCCESS;
            }

            return ETaskState.RUNNING;
        }

        return ETaskState.FAILURE;
    }
}