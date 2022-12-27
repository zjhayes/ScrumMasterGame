using System.Collections;
using System.Collections.Generic;

public class ActionList
{
    List<IAction> actions;

    public List<IAction> Actions
    {
        get { return actions; }
        set { actions = value; }
    }

    public ActionList()
    {
        actions = new List<IAction>();
    }

    // Remove completed actions.
    public void Clean()
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

    // Remove all actions from list.
    public void Clear()
    {
        actions.Clear();
    }

    public bool IsEmpty()
    {
        return actions.Count == 0;
    }
}
