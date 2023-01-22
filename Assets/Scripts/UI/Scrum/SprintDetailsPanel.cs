using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SprintDetailsPanel : MenuController
{
    [SerializeField]
    Button beginSprintButton;

    public delegate void OnBeginSprint();
    public OnBeginSprint onBeginSprint;

    public void BeginSprintPressed()
    {
        onBeginSprint?.Invoke();
    }

    public void UpdateButtonInteraction(bool enable)
    {
        beginSprintButton.interactable = enable;
    }
}
