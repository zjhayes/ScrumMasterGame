using UnityEngine;

public class WorkStation : Station
{ 
    [SerializeField]
    Container cartridgeIntake;

    bool claimed = false; // Character has claimed this station.

    protected override void OnSit(ICharacterController occupant)
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

        claimed = false;
        base.OnSit(occupant);
    }

    protected override void OnStand(ICharacterController occupant)
    {
        if(CurrentCartridge?.Task.Assignee == occupant)
        {
            // Assignee takes cartridge.
            occupant.Inventory.PickUp(CurrentCartridge);
        }
        base.OnStand(occupant);
    }

    private void InputCartridge(ICharacterController character)
    {
        Pickup pickup = character.Inventory.Drop(); // Get pickup.

        if (pickup is Cartridge)
        {
            cartridgeIntake.Add(pickup);
            pickup.EnablePhysics(false);
            pickup.SetPositionToContainer(cartridgeIntake);
            pickup.SetToHoldRotation();
        }
    }

    public bool Claimed
    {
        get { return claimed; }
        set { claimed = value; }
    }

    public Cartridge CurrentCartridge
    {
        get
        {
            return cartridgeIntake.GetFirst<Cartridge>() as Cartridge;
        }
    }
}
