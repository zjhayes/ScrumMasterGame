using UnityEngine;

public class WorkStation : Station
{ 
    [SerializeField]
    Transform cartridgeIntake;

    CharacterController developer;
    CharacterController peerReviewer;
    Cartridge cartridge;

    protected override void OnSit(CharacterController occupant)
    {
        if (!developer)
        {
            developer = occupant;

            if (developer.Inventory.HasPickup())
            {
                InputCartridge(developer);
            }
        }
        else if (!peerReviewer)
        {
            peerReviewer = occupant;

            if (peerReviewer.Inventory.HasPickup())
            {
                peerReviewer.Inventory.Drop();
            }
        }
    }

    protected override void OnStand(CharacterController occupant)
    {
        if (developer == occupant)
        {
            if (cartridge)
            {
                // Take cartridge.
                developer.Inventory.PickUp(cartridge);
                cartridge = null;
            }
            developer = null;
        }
        else if (peerReviewer == occupant)
        {
            peerReviewer = null;
        }
    }

    private void InputCartridge(CharacterController character)
    {
        Pickup pickup = character.Inventory.Drop()[0]; // Get first pickup.

        if (pickup is Cartridge)
        {
            // Disable pickup physics.
            pickup.GetComponent<Rigidbody>().useGravity = false;
            pickup.GetComponent<Rigidbody>().isKinematic = true;
            pickup.GetComponent<Collider>().enabled = false;

            // Move to intake position.
            pickup.transform.parent = cartridgeIntake;
            pickup.transform.position = cartridgeIntake.position;
            pickup.transform.rotation = cartridgeIntake.rotation;

            cartridge = pickup as Cartridge;
        }
    }
}
