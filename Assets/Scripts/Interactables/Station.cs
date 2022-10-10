using UnityEngine;

public class Station : Interactable
{
    [SerializeField]
    private Transform developerSeat;
    [SerializeField]
    private Transform peerSeat;
    [SerializeField]
    private Transform cartridgeIntake;

    public override void Interact(CharacterInteraction invoker)
    {
        base.Interact(invoker);
        CharacterController character = invoker.GetComponent<CharacterController>();
        CharacterInventory inventory = invoker.GetComponent<CharacterInventory>();
        
        if(!Occupied(developerSeat))
        {
            if(inventory.HasPickup())
            {
                InputCartridge(inventory);
            }

            Sit(character, developerSeat);
        }
        else if(!Occupied(peerSeat))
        {
            if(inventory.HasPickup())
            {
                inventory.Drop();
            }

            Sit(character, peerSeat);
        }
    }

    private void Sit(CharacterController character, Transform seat)
    {
        // Disable character physics.
        character.GetComponent<Rigidbody>().useGravity = false;
        character.GetComponent<Rigidbody>().isKinematic = true;
        character.GetComponent<Collider>().enabled = false;

        // Move to seat.
        character.transform.parent = seat;
        character.transform.position = seat.position;
        character.transform.rotation = seat.rotation;
    }

    private void InputCartridge(CharacterInventory inventory)
    {
        Pickup pickup = inventory.Drop()[0]; // Get first pickup.

        // Disable pickup physics.
        pickup.GetComponent<Rigidbody>().useGravity = false;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        pickup.GetComponent<Collider>().enabled = false;

        // Move to intake position.
        pickup.transform.parent = cartridgeIntake;
        pickup.transform.position = cartridgeIntake.position;
        pickup.transform.rotation = cartridgeIntake.rotation;
    }

    private bool Occupied(Transform location)
    {
        if(location.transform.childCount > 0)
        {
            return true;
        }
        return false;
    }
}
