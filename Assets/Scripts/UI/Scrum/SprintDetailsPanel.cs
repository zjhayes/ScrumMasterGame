using UnityEngine;

public class SprintDetailsPanel : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsShowing
    {
        get { return gameObject.active; }
    }
}
