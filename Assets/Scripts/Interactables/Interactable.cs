using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Renderer[] outlineMaterials;

    const string ENABLE_OUTLINE_PROPERTY = "_Enable_Outline";

    void Start()
    {
        ContextManager.Instance.onCharacterSelected += Enable;
        ContextManager.Instance.onDeselect += Disable;
        outlineMaterials = GetComponentsInChildren<Renderer>();
        Disable();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ContextManager.Instance.OnInteractableSelected(this, eventData);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach(Renderer outlineMaterial in outlineMaterials)
        {
            outlineMaterial.material.SetInt(ENABLE_OUTLINE_PROPERTY, 1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach(Renderer outlineMaterial in outlineMaterials)
        {
            outlineMaterial.material.SetInt(ENABLE_OUTLINE_PROPERTY, 0);
        }
    }

    private void Enable()
    {
        this.enabled = true;
        Debug.Log("Enabled " + gameObject.name);
    }

    private void Disable()
    {
        this.enabled = false;
        Debug.Log("Disabled " + gameObject.name);
    }
}