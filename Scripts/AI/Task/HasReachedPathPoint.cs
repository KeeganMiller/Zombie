using System;
using System.Collections.Generic;
using Godot;

public class HasReachedPathPoint : Task
{
    private const float STOPPING_DISTANCE = 30f;
    public HasReachedPathPoint(BehaviorTree tree) : base(tree)
    {
    }

    public HasReachedPathPoint(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if(_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;

        if (bb != null)
        {
            PathController path = _Tree.Owner.FollowPath;
            if (path != null)
            {
                int currentPathIndex = bb.GetValueAsInt("CurrentPathIndex");
                Vector2 pathPos = path.GetNextPathPoint(currentPathIndex);
                if (_Tree.Owner.GlobalPosition.DistanceTo(pathPos) < STOPPING_DISTANCE)
                    return ETaskState.SUCCESS;

            }
        }

        return ETaskState.FAILURE;
    }
}