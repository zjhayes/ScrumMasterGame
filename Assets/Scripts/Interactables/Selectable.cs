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

    private bool canSelect = true;

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

    public bool CanSelect
    {
        get { return canSelect; }
    }

    public void Select()
    {
        if(canSelect)
        {
            onSelect?.Invoke();
        }
    }

    public void HoverEnter()
    {
        if(canSelect)
        {
            onHoverEnter?.Invoke();
        }
    }

    public void HoverExit()
    {
        if(canSelect)
        {
            onHoverExit?.Invoke();
        }
    }

    public void EnableSelection()
    {
        canSelect = true;
        onEnableSelectability?.Invoke();
    }

    public void DisableSelection()
    {
        canSelect = false;
        onDisableSelectability?.Invoke();
    }
}
