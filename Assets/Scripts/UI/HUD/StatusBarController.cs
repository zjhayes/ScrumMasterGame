using UnityEngine;

public class StatusBarController : MenuController
{
    [SerializeField]
    private UIClock clock;

    public override void SetUp()
    {
        gameManager.Sprint.OnBeginSprint += this.Show;
        gameManager.Sprint.OnBeginRetrospective += this.Hide;
        Hide();
    }

    public UIClock Clock
    {
        get { return clock; }
    }
}
