using UnityEngine;
using System.Collections.Generic;

/* Controls the outlines of objects visible on mouse hover. */
public class OutlineController : MonoBehaviour
{
    [SerializeField]
    private List<Outline> outlines = new List<Outline>();
    [SerializeField]
    private Selectable selectability;

    private void Start()
    {
        selectability.onHoverEnter += Show;
        selectability.onHoverExit += Hide;
        selectability.onDisableSelectability += Hide;
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

    private void OnDisable()
    {
        selectability.onHoverEnter -= Show;
        selectability.onHoverExit -= Hide;
        selectability.onDisableSelectability -= Hide;
    }
}