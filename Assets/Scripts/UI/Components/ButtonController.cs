using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public delegate void OnClick();
    public event OnClick onClick;

    public void ButtonClicked()
    {
        onClick?.Invoke();
    }
}
