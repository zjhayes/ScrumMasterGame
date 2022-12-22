using System;
using UnityEngine;

public class GradualAction : IAction
{
    Action<float> action;
    private float start;
    private float stop;
    private float rate;
    private bool done;
    private float currentTime = 0;

    public GradualAction(Action<float> action, float start, float stop, float rate)
    {
        this.action = action;
        this.start = start;
        this.stop = stop;
        this.rate = rate;
        this.done = false;
    }

    public void Run()
    {
        currentTime += Time.deltaTime / rate;
        float lerp = Mathf.Lerp(start, stop, currentTime);
        action(lerp);
        if(currentTime >= 1) { done = true; }
    }

    public bool IsDone()
    {
        return done;
    }

    public void Cancel()
    {
        done = true;
    }
}
