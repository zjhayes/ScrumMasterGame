using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SelectedIcon : MenuController
{
    Image icon;

    public override void SetUp()
    {
        icon = GetComponent<Image>();
        Hide();
    }

    void Update()
    {
        UpdatePosition();
    }

    public override void Show()
    {
        UpdatePosition();
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public override void SetActive(bool active)
    {
        this.enabled = active;
    }

    void UpdatePosition()
    {
        // Set position to current selected character's UI overhead position.
        transform.position = ContextManager.Instance.CurrentCharacter.GetComponent<OverheadController>().GetIconPosition();
    }

    void OnEnable()
    {
        icon.enabled = true;
    }

    void OnDisable()
    {
        icon.enabled = false;
    }

}
