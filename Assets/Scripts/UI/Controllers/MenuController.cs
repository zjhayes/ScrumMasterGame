using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    bool escapable = false;
    [SerializeField]
    bool tint = false; // Include black backdrop?

    public delegate void OnShow(MenuController menu);
    public OnShow onShow;
    public delegate void OnHide(MenuController menu);
    public OnHide onHide;

    public virtual void SetUp()
    {
        // Initialize inactive menu.
    }

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

    public void SetActive(bool active)
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

    public bool Tinted
    {
        get { return tint; }
    }
}
