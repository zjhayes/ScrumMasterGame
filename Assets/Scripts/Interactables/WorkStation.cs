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
                // Character is frustrated, computer is taken.
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

    bool CharacterCanWorkOnTask(ICharacterController character)
    {
        // Station is open for character to work on task, which is not yet ready for production.
        return character.Inventory.TryGetPickup(out Cartridge cartridge) && !cartridge.Task.IsReadyForProduction && this.HasVacancy();
    }

    bool CharacterCanPairProgram(ICharacterController character)
    {
        // Station is open for pair programming, character doesn't have own pickup.
        return this.CountOccupants() == 1;
    }
}
