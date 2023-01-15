using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SprintDetailsPanel : ExpandablePanel
{
    [SerializeField]
    Button beginSprintButton;

    public delegate void OnBeginSprint();
    public OnBeginSprint onBeginSprint;

    public void BeginSprint()
    {
        onBeginSprint?.Invoke();
    }

    public void UpdateButtonInteraction(bool enable)
    {
        beginSprintButton.interactable = enable;
    }
}
