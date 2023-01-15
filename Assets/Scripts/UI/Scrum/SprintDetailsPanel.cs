using UnityEngine;

public class SprintDetailsPanel : ExpandablePanel
{
    public delegate void OnBeginSprint();
    public OnBeginSprint onBeginSprint;

    public void BeginSprint()
    {
        onBeginSprint?.Invoke();
    }

}
