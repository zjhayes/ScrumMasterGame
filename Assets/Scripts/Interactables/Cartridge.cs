using UnityEngine;

public class Cartridge : Pickup
{
    [SerializeField]
    private CharacterController assignee;

    public CharacterController Assignee
    {
        get { return assignee; }
    }
}
