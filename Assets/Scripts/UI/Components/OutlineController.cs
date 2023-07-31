using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Selectable))]
public class OutlineController : MonoBehaviour
{
    Selectable interactable;
    Outline outline;

    void Awake()
    {
        interactable = GetComponent<Selectable>();
        outline = GetComponent<Outline>();
    }

    void Start()
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
