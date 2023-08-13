using UnityEngine;

public class StatusBarController : MenuController
{
    [SerializeField]
    private UIClock clock;

    public override void SetUp()
    {
        gameManager.Sprint.onBeginSprint += this.Show;
        gameManager.Sprint.onBeginRetrospective += this.Hide;
        Hide();
    }

    public UIClock Clock
    {
        get { return clock; }
    }
}
