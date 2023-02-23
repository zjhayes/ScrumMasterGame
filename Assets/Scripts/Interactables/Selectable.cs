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

    public delegate void OnEnable();
    public OnEnable onEnable;

    public delegate void OnDisable();
    public OnDisable onDisable;

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
        onSelect?.Invoke();
    }

    protected virtual void HoverEnter()
    {
        onHoverEnter?.Invoke();
    }

    protected virtual void HoverExit()
    {
        onHoverExit?.Invoke();
    }

    protected virtual void Enable()
    {
        this.enabled = true;
        onEnable?.Invoke();
    }

    protected virtual void Disable()
    {
        this.enabled = false;
        onDisable?.Invoke();
    }
}
