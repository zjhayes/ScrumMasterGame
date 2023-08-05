using UnityEngine;
using UnityEngine.UI;

public class SprintDetailsPanel : MenuController
{
    [SerializeField]
    private Button beginSprintButton;

    public delegate void OnBeginSprint();
    public OnBeginSprint onBeginSprint;

    public override void SetUp()
    {
        // Nothing to do.
    }

    public void BeginSprintPressed()
    {
        onBeginSprint?.Invoke();
    }

    public void UpdateButtonInteraction(bool enable)
    {
        beginSprintButton.interactable = enable;
    }
}
