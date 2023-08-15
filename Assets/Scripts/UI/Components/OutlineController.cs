using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Selectable))]
public class OutlineController : MonoBehaviour
{
    [SerializeField]
    private List<Outline> outlines = new List<Outline>();

    private Selectable selectable;

    private void Awake()
    {
        selectable = GetComponent<Selectable>();
    }

    private void Start()
    {
        selectable.onHoverEnter += Show;
        selectable.onHoverExit += Hide;
        selectable.onDisableSelectability += Hide;
        Hide();
    }

    public void Show()
    {
        // Enable outlines.
        outlines.ForEach(outline => outline.enabled = true);
    }

    public void Hide()
    {
        // Disable outlines.
        outlines.ForEach(outline => outline.enabled = false);
    }
}
