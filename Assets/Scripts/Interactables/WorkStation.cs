using UnityEngine;

[RequireComponent(typeof(TaskComputer))]
public class WorkStation : Station
{ 
    TaskComputer computer;

    void Awake()
    {
        computer = GetComponent<TaskComputer>();
    }

    protected override void OnSit(ICharacterController occupant)
    {
        // Get cartridge from character.
        if(occupant.Inventory.HasPickup())
        {
            if(computer.CartridgeIntake.IsEmpty && occupant.Inventory.CurrentPickup is Cartridge)
            {
                RunCartridge(occupant.Inventory.CurrentPickup as Cartridge);
            }
            else
            {
                // Character drops pickup when not cartridge or intake is full.
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

    private void RunCartridge(Cartridge cartridge)
    {
        computer.InputCartridge(cartridge);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if (character.Inventory.CurrentPickup is Cartridge && this.HasVacancy()) //TODO: Find another way to determine character has task
        {
            // Character can work on task.
            return PriorityScoreConstants.WORK_ON_TASK;
        }
        else if(!character.Inventory.HasPickup() && this.CountOccupants() == 1)
        {
            // Character can pair program.
            return 20;
        }
        return 0;
    }

    public Cartridge CurrentCartridge
    {
        get
        {
            return computer.CartridgeIntake.GetFirst<Cartridge>() as Cartridge;
        }
    }
}
