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

        Blackboard bb = _Tree.BlackboardRef;                // Get reference to the blackboard
        if (bb != null)
        {
            Node2D self = bb.GetValueAsNode("Self");            // Get self reference
            if (self != null)
            {
                Vector2 currentPos = self.GlobalPosition;                   // Get the agents current position
                Vector2 moveToPos = bb.GetValueAsVector2("MoveToLocation");         // Get the position we wish to move to
                
                // Check if we are in stopping distance of the location, if not than set the location
                // Otherwise we have reached our location
                if (currentPos.DistanceTo(moveToPos) > STOPPING_DISTANCE)
                {
                    if (self is BaseCharacterController character)
                    {
                        character.Agent?.SetTargetLocation(moveToPos);
                        return ETaskState.RUNNING;
                    }
                    else
                    {
                        GD.PrintErr("#MoveToLocation-Task: Failed to cast to character controller");
                    }
                }
                else
                {
                    bb.SetValueAsBool("HasMoveToLocation", false);
                    bb.SetValueAsVector2("MoveToLocation", Vector2.Zero);
                    return ETaskState.SUCCESS;
                }
            }
        }

        return ETaskState.FAILURE;
    }
}