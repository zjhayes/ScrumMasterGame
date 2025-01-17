
using UnityEngine;

public class PauseMenuController : MenuController
{
    [SerializeField]
    ButtonController resumeButton;

    public override void SetUp()
    {
        Hide();
    }

    public override void Show()
    {
        SetActive(true);
        base.Show();
    }

    public override void Hide()
    {
        SetActive(false);
        base.Hide();
    }

    public ButtonController ResumeButton
    {
        get { return resumeButton; }
    }
}
