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

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        // Set position to current selected character's UI overhead position.
        transform.position = ContextManager.Instance.CurrentCharacter.GetComponent<OverheadController>().GetIconPosition();
    }

    public void Show()
    {
        UpdatePosition();
        this.enabled = true;
        icon.enabled = true;
    }

    public void Hide()
    {
        icon.enabled = false;
        this.enabled = false;
    }
}
