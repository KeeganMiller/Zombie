using System;
using System.Collections.Generic;
using Godot;

public enum EFollowDirection
{
    FORWARDS,
    BACKWARDS
}

public class SetNextPathPoint : Task
{
    private EFollowDirection _CurrentDirection = EFollowDirection.FORWARDS;
    
    public SetNextPathPoint(BehaviorTree tree) : base(tree)
    {
    }

    public SetNextPathPoint(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            int currentIndex = bb.GetValueAsInt("CurrentPathIndex");
            PathController path = _Tree.Owner.FollowPath;
            if (path != null)
            {
                if (!bb.GetValueAsBool("HasPathPoint"))
                {
                    bb.SetValueAsInt("CurrentPathIndex", currentIndex);
                    bb.SetValueAsVector2("MoveToLocation", path.GetNextPathPoint(currentIndex));
                    bb.SetValueAsBool("HasPathPoint", true);
                    return ETaskState.SUCCESS;
                }
                
                if (!_Tree.Owner.CirclePath)
                {
                    EFollowDirection direction = _Tree.Owner.FollowDirection;
                    if (direction == EFollowDirection.FORWARDS)
                    {
                        currentIndex++;                 // increment the path
                        if (currentIndex == path.GetPathCount)
                        {
                            _Tree.Owner.FollowDirection = EFollowDirection.BACKWARDS;
                            currentIndex -= 2;
                        }
                    }
                    else
                    {
                        currentIndex--;
                        if (currentIndex < 0)
                        {
                            _Tree.Owner.FollowDirection = EFollowDirection.FORWARDS;
                            currentIndex += 2;
                        }
                    }
                }
                else
                {
                    currentIndex += 1;
                    if (currentIndex == path.GetPathCount)
                        currentIndex = 0;
                }
                
                bb.SetValueAsInt("CurrentPathIndex", currentIndex);
                Vector2 moveTo = path.GetNextPathPoint(currentIndex);
                GD.Print(moveTo);
                bb.SetValueAsVector2("MoveToLocation", moveTo);
                return ETaskState.SUCCESS;
            }

            
        }
        
        return ETaskState.FAILURE;
    }
}