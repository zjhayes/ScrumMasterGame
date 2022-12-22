using System;
using UnityEngine;

public class RepeatedAction : BasicAction
{
    private int numberOfRuns;
    private int currentRun;

    public RepeatedAction(Action action, int numberOfRuns) : base(action)
    {
        this.numberOfRuns = numberOfRuns;
        this.currentRun = 0;
    }

    public override void Run()
    {
        action();
        currentRun++;
        if(currentRun >= numberOfRuns) { done = true; }
    }
}
