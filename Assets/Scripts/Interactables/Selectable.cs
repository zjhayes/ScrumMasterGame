using UnityEngine;
using UnityEngine.EventSystems;

public class Selectable : GameBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void OnSelect();
    public OnSelect onSelect;

    public delegate void OnHoverEnter();
    public OnHoverEnter onHoverEnter;

    public delegate void OnHoverExit();
    public OnHoverExit onHoverExit;

    public delegate void OnEnableSelectability();
    public OnEnableSelectability onEnableSelectability;
    public delegate void OnDisableSelectability();
    public OnDisableSelectability onDisableSelectability;

    bool canSelect = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverExit();
    }

    protected virtual void Select()
    {
        if(canSelect)
        {
            onSelect?.Invoke();
        }
    }

    protected virtual void HoverEnter()
    {
        if(canSelect)
        {
            onHoverEnter?.Invoke();
        }
    }

    protected virtual void HoverExit()
    {
        if(canSelect)
        {
            onHoverExit?.Invoke();
        }
    }

    protected virtual void EnableSelection()
    {
        canSelect = true;
        onEnableSelectability?.Invoke();
    }

    protected virtual void DisableSelection()
    {
        canSelect = false;
        onDisableSelectability?.Invoke();
    }

    public bool CanSelect
    {
        get { return canSelect; }
    }
}
