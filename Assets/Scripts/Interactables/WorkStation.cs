using UnityEngine;

public class WorkStation : Station
{ 
    [SerializeField]
    Container cartridgeIntake;
    
    protected override void OnSit(CharacterController occupant)
    {
        // Get cartridge from character.
        if(occupant.Inventory.HasPickup())
        {
            if(cartridgeIntake.IsEmpty)
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
        if(CurrentCartridge?.Assignee == occupant)
        {
            // Assignee takes cartridge.
            occupant.Inventory.PickUp(CurrentCartridge);
        }
        base.OnStand(occupant);
    }

    private void InputCartridge(CharacterController character)
    {
        Pickup pickup = character.Inventory.Drop(); // Get pickup.

        if (pickup is Cartridge)
        {
            cartridgeIntake.Add(pickup);
        }
    }

    private Cartridge CurrentCartridge
    {
        get
        {
            return cartridgeIntake.Get<Cartridge>(true) as Cartridge;
        }
    }
}
