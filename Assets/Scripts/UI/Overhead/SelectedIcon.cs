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
        Debug.Log("Should not update when no character selected.");
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

    void UpdatePosition()
    {
        // Set position to current selected character's UI overhead position.
        transform.position = ContextManager.Instance.CurrentCharacter.GetComponent<OverheadController>().GetIconPosition();
    }
}
