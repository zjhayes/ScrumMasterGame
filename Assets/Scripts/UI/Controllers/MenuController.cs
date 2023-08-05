using UnityEngine;

public abstract class MenuController : GameBehaviour
{
    [SerializeField]
    bool escapable = false;

    public delegate void OnShow(MenuController menu);
    public OnShow onShow;
    public delegate void OnHide(MenuController menu);
    public OnHide onHide;

    public abstract void SetUp();

    public virtual void Show()
    {
        onShow?.Invoke(this);
    }

    public virtual void Hide()
    {
        onHide?.Invoke(this);
    }

    public virtual void Escape()
    {
        if(escapable)
        {
            Hide();
        }
    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public bool IsShowing
    {
        get { return gameObject.activeInHierarchy; }
    }

    public bool Escapable
    {
        get { return escapable; }
    }
}
