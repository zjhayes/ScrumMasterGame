using UnityEngine;

[RequireComponent(typeof(TaskComputer))]
public class WorkStation : Station
{
    private TaskComputer computer;

    private void Awake()
    {
        computer = GetComponent<TaskComputer>();

        // Dismiss developers when task is completed or removed.
        computer.onTaskComplete += DismissAll;
        computer.onSleep += DismissAll;
    }

    public override void InteractWith(ICharacterController character)
    {
        if(CharacterCanWorkOnTask(character) || CharacterCanPairProgram(character))
        {
            base.InteractWith(character);
        }
        else
        {
            character.Frustrated(); // No reason to interact.

        }
    }

    protected override void FindSeat(ICharacterController occupant)
    {
        // Handle character pickups before sitting.
        if(occupant.Inventory.HasPickup())
        {
            // Check if character has a cartridge, and the computer is not in use.
            if(!computer.HasCartridge() && occupant.Inventory.TryGetPickup(out Cartridge cartridge))
            {
                // Slot cartridge into open computer intake.
                computer.InputCartridge(cartridge);
            }
            else
            {
                // Character is frustrated, computer is taken.
                occupant.Frustrated();
                return; // Don't sit.
            }
        }
        base.FindSeat(occupant);
    }

    protected override void OnSit(ICharacterController occupant, Chair chair)
    {
        base.OnSit(occupant, chair);
        computer.SignInDeveloper(occupant);
    }

    protected override void OnStand(ICharacterController occupant)
    {
        if(computer.TryGetCartridge(out Cartridge cartridge) && cartridge.Task.Assignee == occupant)
        {
            // Assignee takes cartridge.
            occupant.Inventory.PickUp(cartridge);
        }

        computer.SignOutDeveloper(occupant);
        base.OnStand(occupant);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if(CharacterCanWorkOnTask(character))
        {
            // Advertise that character can work on task here.
            return PriorityScoreConstants.WORK_ON_TASK;
        }
        else if(CharacterCanPairProgram(character) && !character.Inventory.HasPickup())
        {
            // Advertise that character can pair program here.
            return PriorityScoreConstants.PAIR_PROGRAM;
        }

        return PriorityScoreConstants.NO_SCORE;
    }

    private bool CharacterCanWorkOnTask(ICharacterController character)
    {
        // Station is open for character to work on task, which is not yet ready for production.
        return character.Inventory.TryGetPickup(out Cartridge cartridge) && !cartridge.Task.IsReadyForProduction && this.HasVacancy();
    }

    private bool CharacterCanPairProgram(ICharacterController character)
    {
        // Station is open for pair programming, character doesn't have own pickup.
        return this.CountOccupants() == 1;
    }
}
