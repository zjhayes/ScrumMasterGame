using UnityEngine;

public class PanelController : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsShowing
    {
        get { return gameObject.activeSelf; }
    }
}
