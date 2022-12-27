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
        Debug.Log("Total Actions: " + actionList.Actions.Count);
        foreach (IAction action in actionList.Actions)
        {
            action.Run();
        }
        actionList.Clean();
        AddPendingActions();
        DisableIfNothingToDo();
    }

    public void Add(IAction action)
    {
        pendingActions.Actions.Add(action);
        EnableIfDisabled();
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

    void DisableIfNothingToDo()
    {
        if(actionList.IsEmpty() && pendingActions.IsEmpty())
        {
            this.enabled = false;
        }
    }

    void EnableIfDisabled()
    {
        if(!this.enabled)
        {
            this.enabled = true;
        }
    }
}