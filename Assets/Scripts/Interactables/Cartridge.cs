using UnityEngine;

public class Cartridge : Pickup
{
    [SerializeField]
    private CharacterController assignee;

    public override void Interact(CharacterController invoker)
    {
        base.Interact(invoker);

        if(assignee == null)
        {
            assignee = invoker.GetComponent<CharacterController>();
        }
    }
}
