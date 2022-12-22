using System;
using UnityEngine;

public class DelayedAction : BasicAction
{
    private float delay;
    private float currentTime;

    public DelayedAction(Action action, float delay) : base(action)
    {
        this.delay = delay;
        this.currentTime = delay;
    }

    public override void Run()
    {
        // Countdown to action.
        currentTime -= Time.deltaTime;
        if(currentTime <= 0.0f)
        {
            action();
            done = true;
        }
    }

    public void Reset()
    {
        currentTime = delay;
    }

    public float CurrentTime { get; set; }
}