using System;
using UnityEngine;

public class Cooldown
{
    private float delay;
    private DelayedAction cooldownAction;
    private bool isReady = true;

    public bool IsReady
    {
        get{ return isReady; }
    }

    public delegate void OnCooldownReady();
    public OnCooldownReady onCooldownReady;


    public Cooldown(float delay)
    {
        this.delay = delay;
    }

    public void Begin()
    {
        if(isReady)
        {
            // Start new cooldown.
            isReady = false;
            cooldownAction = new DelayedAction(End, delay);
            ActionManager.Instance.Add(cooldownAction);
            InvokeCooldownEvent();
        }
        else
        {
            // Restart current cooldown.
            Reset();
        }
    }

    public void Reset()
    {
        // Restart cooldown if current action exists.
        if(cooldownAction != null && !cooldownAction.IsDone())
        {
            cooldownAction.Reset();
        }
    }

    public void Cancel()
    {
        // Immediately ends current cooldown.
        if(cooldownAction != null && !cooldownAction.IsDone())
        {
            cooldownAction.Cancel();
            End();
        }
    }

    private void End()
    {
        isReady = true;
    }

    private void InvokeCooldownEvent()
    {
        onCooldownReady?.Invoke();
    }
}