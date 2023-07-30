using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Interactable))]
public class OutlineController : MonoBehaviour
{
    Interactable interactable;
    Outline outline;

    void Awake()
    {
        interactable = GetComponent<Interactable>();
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
