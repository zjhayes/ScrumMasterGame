using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class OutlineController : MonoBehaviour
{
    OutlineRenderer[] outlineMaterials;
    Interactable interactable;

    void Awake()
    {
        outlineMaterials = GetComponentsInChildren<OutlineRenderer>();
        interactable = GetComponent<Interactable>();
    }

    void Start()
    {
        interactable.onHoverEnter += Show;
        interactable.onHoverExit += Hide;
        interactable.onDisable += Hide;
    }

    public void Show()
    {
        foreach (OutlineRenderer outlineMaterial in outlineMaterials)
        {
            outlineMaterial.Show();
        }
    }

    public void Hide()
    {
        foreach (OutlineRenderer outlineMaterial in outlineMaterials)
        {
            outlineMaterial.Hide();
        }
    }
}
