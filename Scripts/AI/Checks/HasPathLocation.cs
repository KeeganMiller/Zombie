using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Godot;


public class HasPathLocationTask : Task
{
    private bool _RequiresLocation;
    public HasPathLocationTask(BehaviorTree tree, bool requiresLoc) : base(tree)
    {
        _RequiresLocation = requiresLoc;
    }

    public HasPathLocationTask(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            if (_RequiresLocation)
            {
                if (bb.GetValueAsBool("HasPathPoint"))
                    return ETaskState.SUCCESS;
            }
            else
            {
                if (bb.GetValueAsBool("HasPathPoint") == false)
                    return ETaskState.SUCCESS;
            }
        }

        return ETaskState.FAILURE;
    }
}