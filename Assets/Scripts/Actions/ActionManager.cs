using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : Singleton<ActionManager>
{
    ActionList actionList;
    ActionList pendingActions;

    protected override void Awake()
    {
        base.Awake();
        actionList = new ActionList();
        pendingActions = new ActionList();
    }

    void Update()
    {
        foreach (IAction action in actionList.Actions)
        {
            action.Run();
        }
        actionList.Clean();
        AddPendingActions();
    }

    public void Add(IAction action)
    {
        pendingActions.Actions.Add(action);
    }

    private void AddPendingActions()
    {
        foreach (IAction action in pendingActions.Actions)
        {
            actionList.Actions.Add(action);
        }
        pendingActions.Clear();
    }

    // Set all actions as done.
    public void CancelAll()
    {
        AddPendingActions();
        foreach (IAction action in actionList.Actions)
        {
            action.Cancel();
        }
        actionList.Clear();
    }

    void OnDisable()
    {
        CancelAll();
    }
}