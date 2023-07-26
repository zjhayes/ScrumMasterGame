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
                computer.InputCartridge(occupant.Inventory.CurrentPickup as Cartridge);
            }
            else
            {
                // Character drops pickup and is frustrated.
                occupant.Inventory.Drop();
                occupant.Frustrated();
                return;
            }
        }
        base.OnSit(occupant);
    }

    protected override void OnStand(ICharacterController occupant)
    {
        if(computer.CurrentCartridge?.Task.Assignee == occupant)
        {
            // Assignee takes cartridge.
            occupant.Inventory.PickUp(computer.CurrentCartridge);
        }
        base.OnStand(occupant);
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
}
