using System;
using System.Collections.Generic;
using Godot;

public class SettlerTree : BehaviorTree
{
    public SettlerTree(Blackboard blackboard, BaseCharacterController owner) : base(blackboard, owner)
    {
    }

    protected override Task CreateTree()
    {
        return new Selector(this, new List<Task>
        {
            new Sequence(this, new List<Task>
            {
                new IsWaiting(this),
                new WaitTask(this)
            }),
            new Sequence(this, new List<Task>
            {
                new MovementStateCheck(this, EMovementState.FOLLOW_PATH),
                new Selector(this, new List<Task>
                {
                    new Sequence(this, new List<Task>
                    {
                        new HasPathLocationTask(this, false),
                        new SetNextPathPoint(this),
                        new SetWaitTask(this, 2.0f)
                    }),
                    new Sequence(this, new List<Task>()
                    {
                        new HasReachedPathPoint(this),
                        new SetNextPathPoint(this)
                    }),
                    new Sequence(this, new List<Task>
                    {
                        new HasPathLocationTask(this, true),
                        new MoveToLocationTask(this)
                    })
                })
            }),
            new Sequence(this, new List<Task>
            {
                new MoveToLocationCheck(this),
                new MoveToLocationTask(this),
                new SetWaitTask(this, 2.0f)
            })
        });
    }
}