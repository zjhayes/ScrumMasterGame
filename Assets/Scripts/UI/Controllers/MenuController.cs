using UnityEngine;

public abstract class MenuController : GameBehaviour
{
    [SerializeField]
    private bool escapable = false;

    public Events.MenuEvent<MenuController> OnShow;
    public Events.MenuEvent<MenuController> OnHide;

    public abstract void SetUp();

    public virtual void Show()
    {
        OnShow?.Invoke(this);
    }

    public virtual void Hide()
    {
        OnHide?.Invoke(this);
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
