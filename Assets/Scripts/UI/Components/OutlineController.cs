using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Selectable))]
public class OutlineController : MonoBehaviour
{
    private Selectable interactable;
    private Outline outline;

    private void Awake()
    {
        interactable = GetComponent<Selectable>();
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        interactable.onHoverEnter += Show;
        interactable.onHoverExit += Hide;
        interactable.onDisableSelectability += Hide;
        Hide();
    }

    public void Show()
    {
        outline.enabled = true;
    }

    public void Hide()
    {
        outline.enabled = false;
    }
}
