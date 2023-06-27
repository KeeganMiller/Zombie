using System;
using System.Collections.Generic;
using Godot;

public enum ETaskState
{
    SUCCESS = 0,
    RUNNING = 1,
    FAILURE = 2
}

public abstract class Task
{
    public Task Parent;
    protected List<Task> _Children = new List<Task>();

    protected BehaviorTree _Tree;

    public Task(BehaviorTree tree)
    {
        _Tree = tree;
    }

    public Task(BehaviorTree tree, List<Task> children)
    {
        _Tree = tree;
        foreach (var child in children)
            AttachChild(child);
    }

    protected void AttachChild(Task child)
    {
        child.Parent = this;
        _Children.Add(child);
    }

    public abstract ETaskState RunTask(float delta);
}