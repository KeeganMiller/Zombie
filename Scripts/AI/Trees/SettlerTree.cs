using System;
using System.Collections.Generic;
using Godot;

public class SettlerTree : BehaviorTree
{
    public SettlerTree(Blackboard blackboard) : base(blackboard)
    {
    }

    protected override Task CreateTree()
    {
        return new Selector(this, new List<Task>
        {
            new Sequence(this, new List<Task>
            {
                new MoveToLocationCheck(this),
                new MoveToLocationTask(this)
            })
        });
    }
}