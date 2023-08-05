using UnityEngine;

public class InSprintPanel : MenuController
{
    [SerializeField]
    Container container;

    public override void SetUp()
    {
        // Nothing to do.
    }

    public Container Container
    {
        get { return container; }
    }
}
