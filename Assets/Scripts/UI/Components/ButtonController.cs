using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public event Events.PlayerEvent OnClick;

    // Add this method to Button Unity Events.
    public void ButtonClicked()
    {
        OnClick?.Invoke();
    }
}
