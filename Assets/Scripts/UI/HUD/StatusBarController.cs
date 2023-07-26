using UnityEngine;

public class StatusBarController : MenuController
{
    [SerializeField]
    UIClock clock;

    public override void SetUp()
    {
        
    }

    public UIClock Clock
    {
        get { return clock; }
    }
}
