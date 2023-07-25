using UnityEngine;

public class WorkStation : Station
{ 
    [SerializeField]
    Container cartridgeIntake;

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

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if(character.Inventory.CurrentPickup is Cartridge && this.HasVacancy()) //TODO: Find another way to determine character has task
        {
            // Character can work on task.
            return 100;
        }
        else if(!character.Inventory.HasPickup() && this.CountOccupants() == 1)
        {
            // Character can pair program.
            return 60;
        }
        return 0;
    }

    public Cartridge CurrentCartridge
    {
        get
        {
            return cartridgeIntake.GetFirst<Cartridge>() as Cartridge;
        }
    }
}
