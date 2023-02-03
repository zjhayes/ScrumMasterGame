using UnityEngine;

public class Cartridge : Pickup
{
    [SerializeField]
    private ICharacterController assignee;

    public ICharacterController Assignee
    {
        get { return assignee; }
    }
}
