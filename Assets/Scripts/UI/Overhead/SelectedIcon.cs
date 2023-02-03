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

    // Override to disable components rather than game object.
    public override void SetActive(bool active)
    {
        this.enabled = active;
    }

    void UpdatePosition()
    {
        // Set position to current selected character's UI overhead position.
        transform.position = gameManager.Context.CurrentCharacter.OverHead.GetIconPosition();
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
