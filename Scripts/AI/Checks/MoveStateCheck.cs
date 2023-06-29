using System;
using System.Collections.Generic;
using Godot;

public class MovementStateCheck : Task
{
    private EMovementState _CheckingState;
    public MovementStateCheck(BehaviorTree tree, EMovementState state) : base(tree)
    {
        _CheckingState = state;
    }

    public MovementStateCheck(BehaviorTree tree, List<Task> children) : base(tree, children)
    {
        
    }

    public override ETaskState RunTask(float delta)
    {
        if (_Tree == null)
            return ETaskState.FAILURE;

        Blackboard bb = _Tree.BlackboardRef;
        if (bb != null)
        {
            EMovementState currentState = (EMovementState)bb.GetValueAsInt("MovementState");
            if (currentState == _CheckingState)
                return ETaskState.SUCCESS;
        }

        return ETaskState.FAILURE;
    }
}