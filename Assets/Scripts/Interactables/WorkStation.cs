using UnityEngine;

public class WorkStation : Station
{ 
    [SerializeField]
    Transform cartridgeIntake;

    Cartridge cartridge;
    
    protected override void OnSit(CharacterController occupant)
    {
        // Get cartridge from character.
        if(occupant.Inventory.HasPickup())
        {
            if(!cartridge)
            {
                InputCartridge(occupant);
            }
            else
            {
                // Character drops pickup if one present already.
                occupant.Inventory.Drop();
            }
        }
        base.OnSit(occupant);
    }

    protected override void OnStand(CharacterController occupant)
    {
        if(cartridge != null && cartridge.Assignee == occupant)
        {
            // Assignee takes cartridge.
            occupant.Inventory.PickUp(cartridge);
            cartridge = null;
        }
        base.OnStand(occupant);
    }

    private void InputCartridge(CharacterController character)
    {
        Pickup pickup = character.Inventory.Drop(); // Get pickup.

        if (pickup is Cartridge)
        {
            // Disable pickup physics.
            pickup.EnablePhysics(false);

            // Move to intake position.
            pickup.transform.parent = cartridgeIntake;
            pickup.transform.position = cartridgeIntake.position;
            pickup.transform.rotation = cartridgeIntake.rotation;

            cartridge = pickup as Cartridge;
        }
    }
}
