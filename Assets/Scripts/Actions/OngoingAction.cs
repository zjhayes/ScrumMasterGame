using System;
using UnityEngine;

public class OngoingAction : BasicAction
{
    public OngoingAction(Action action) : base(action)
    {
        // Ensure Cancel() is manually called to end this action.
    }

    public override void Run()
    {
        action();
    }
}
