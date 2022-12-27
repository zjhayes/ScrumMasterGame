using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectedIcon : MonoBehaviour
{
    Image icon;

    void Awake()
    {
        icon = GetComponent<Image>();
    }

    void Start()
    {
        ContextManager.Instance.onCharacterSelected += Show;
        ContextManager.Instance.onDeselect += Hide;
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

    void Show()
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
