using UnityEngine;
using TMPro;

public class StatusBarController : MenuController
{
    [SerializeField]
    private UIClock clock;
    [SerializeField]
    private TextMeshProUGUI userCountText;

    public override void SetUp()
    {
        gameManager.Sprint.OnBeginSprint += Show;
        gameManager.Sprint.OnBeginRetrospective += Hide;
        Hide();
    }

    public override void Show()
    {
        clock.enabled = true;
        base.Show();
    }

    public override void Hide()
    {
        UpdateUserCount();
        clock.enabled = false;
        base.Hide();
    }

    public void UpdateUserCount()
    {
        userCountText.text = gameManager.Production.UserCount.ToString();
    }

    public UIClock Clock
    {
        get { return clock; }
    }
}
