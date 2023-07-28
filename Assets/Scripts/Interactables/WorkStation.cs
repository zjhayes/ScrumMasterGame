using UnityEngine;

[RequireComponent(typeof(TaskComputer))]
public class WorkStation : Station
{
    TaskComputer computer;

    void Awake()
    {
        computer = GetComponent<TaskComputer>();

        // Dismiss developers when task is completed or removed.
        computer.onTaskComplete += DismissAll;
        computer.onSleep += DismissAll;
    }

    protected override void Sit(ICharacterController occupant)
    {
        // Get cartridge from character.
        if(occupant.Inventory.HasPickup())
        {
            if(computer.CartridgeIntake.IsEmpty && occupant.Inventory.CurrentPickup is Cartridge)
            {
                // Slot cartridge into computer intake.
                computer.InputCartridge(occupant.Inventory.CurrentPickup as Cartridge);
            }
            else
            {
                // Character drops pickup and is frustrated.
                occupant.Inventory.Drop();
                occupant.Frustrated();
                return; // Don't sit.
            }
        }
        base.Sit(occupant);
    }

    protected override void OnSit(ICharacterController occupant)
    {
        computer.SignInDeveloper(occupant);
    }

    protected override void OnStand(ICharacterController occupant)
    {
        if(computer.CurrentCartridge != null && computer.CurrentCartridge.Task.Assignee == occupant)
        {
            // Assignee takes cartridge.
            occupant.Inventory.PickUp(computer.CurrentCartridge);
        }

        computer.SignOutDeveloper(occupant);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if (character.Inventory.CurrentPickup is Cartridge && this.HasVacancy())
        {
            // Station is open for character to work on task.
            return PriorityScoreConstants.WORK_ON_TASK;
        }
        else if(!character.Inventory.HasPickup() && this.CountOccupants() == 1)
        {
            // Station is open for pair programming.
            return PriorityScoreConstants.PAIR_PROGRAM;
        }

        return PriorityScoreConstants.NO_SCORE;
    }
}
