using System;

public class BasicAction : IAction
{
    protected Action action;
    protected bool done;

    public BasicAction(Action action)
    {
        this.action = action;
        this.done = false;
    }

    public virtual void Run()
    {
        action();
        done = true;
    }

    public virtual bool IsDone()
    {
        return done;
    }

    public virtual void Cancel()
    {
        done = true;
    }
}