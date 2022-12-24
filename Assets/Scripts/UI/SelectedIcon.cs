using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectedIcon : MonoBehaviour
{
    Image icon;

    void Awake()
    {
        icon = GetComponent<Image>();
        Hide();
    }

    public void Show()
    {
        icon.enabled = true;
    }

    public void Hide()
    {
        icon.enabled = false;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
