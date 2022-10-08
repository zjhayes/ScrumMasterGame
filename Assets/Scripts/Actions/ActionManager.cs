using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    List<IAction> actions;
    List<IAction> pendingActions;

    protected override void Awake()
    {
        base.Awake();
        CleanAll();
    }

    void Update()
    {
        foreach (IAction action in actions)
        {
            action.Run();
        }
        Clean();
        AddPendingActions();
    }

    public void Add(IAction action)
    {
        pendingActions.Add(action);
    }

    private void AddPendingActions()
    {
        foreach (IAction action in pendingActions)
        {
            actions.Add(action);
        }
        pendingActions.Clear();
    }

    private void Clean()
    {
        List<IAction> actionsClone = new List<IAction>(actions);
        foreach (IAction action in actionsClone)
        {
            if (action.IsDone())
            {
                actions.Remove(action);
            }
        }
    }

    public void CleanAll()
    {
        actions = new List<IAction>();
        pendingActions = new List<IAction>();
    }
}