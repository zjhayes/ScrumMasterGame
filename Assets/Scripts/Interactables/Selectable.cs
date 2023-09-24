using UnityEngine.EventSystems;
using UnityEngine;

/* Give objects the ability to be selected by the player. */
public class Selectable : GameBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Events.PlayerEvent OnSelect;
    public event Events.PlayerEvent OnHoverEnter;
    public event Events.PlayerEvent OnHoverExit;
    public event Events.GameEvent OnEnableSelectability;
    public event Events.GameEvent OnDisableSelectability;

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
            OnSelect?.Invoke();
        }
    }

    public void HoverEnter()
    {
        if (canSelect)
        {
            OnHoverEnter?.Invoke();
        }
    }

    public void HoverExit()
    {
        if(canSelect)
        {
            OnHoverExit?.Invoke();
        }
    }

    public void EnableSelection()
    {
        canSelect = true;
        OnEnableSelectability?.Invoke();
    }

    public void DisableSelection()
    {
        canSelect = false;
        OnDisableSelectability?.Invoke();
    }
}
