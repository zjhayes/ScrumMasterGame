
public class PauseMenuController : MenuController
{

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
}
