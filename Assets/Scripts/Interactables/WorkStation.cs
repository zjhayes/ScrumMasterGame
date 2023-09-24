using UnityEngine;

[RequireComponent(typeof(TaskComputer))]
public class WorkStation : Station
{
    private TaskComputer computer;

    private void Awake()
    {
        computer = GetComponent<TaskComputer>();

        // Dismiss developers when task is completed or removed.
        computer.OnSleep += DismissAll;

        foreach(Chair chair in chairs)
        {
            chair.OnSit += computer.SignInDeveloper;
            chair.OnStand += computer.SignOutDeveloper;
        }
    }

    public override void InteractWith(ICharacterController character)
    {
        if(CharacterCanWorkOnTask(character) || CharacterCanPairProgram(character))
        {
            // Character can either work on their own task, or help the current developer.
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
        if(occupant.Inventory.Has<Cartridge>())
        {
            // Check if character has a cartridge, and the computer is not in use.
            if(!computer.HasCartridge() && occupant.Inventory.TryGet(out Cartridge cartridge))
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

    protected override void OnChairOccupied(ICharacterController occupant)
    {
        computer.SignInDeveloper(occupant);
        base.OnChairOccupied(occupant);
    }

    protected override void OnChairUnoccupied(ICharacterController occupant)
    {
        if(computer.TryGetCartridge(out Cartridge cartridge) && cartridge.Task.Assignee == occupant)
        {
            // Assignee takes cartridge.
            occupant.GoInteractWith(cartridge);
        }

        computer.SignOutDeveloper(occupant);
        base.OnChairUnoccupied(occupant);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if(CharacterCanWorkOnTask(character))
        {
            // Advertise that character can work on task here.
            return PriorityScoreConstants.WORK_ON_TASK;
        }
        else if(CharacterCanPairProgram(character) && !character.Inventory.Has<Cartridge>())
        {
            // Advertise that character can pair program here.
            return PriorityScoreConstants.PAIR_PROGRAM;
        }

        return PriorityScoreConstants.NO_SCORE;
    }

    private bool CharacterCanWorkOnTask(ICharacterController character)
    {
        // Station is open for character to work on task, which is not yet ready for production.
        return character.Inventory.TryGet(out Cartridge cartridge) && !cartridge.Task.IsReadyForProduction && this.HasVacancy();
    }

    private bool CharacterCanPairProgram(ICharacterController character)
    {
        // Station is open for pair programming, character doesn't have own pickup.
        return this.CountOccupants() == 1;
    }
}
