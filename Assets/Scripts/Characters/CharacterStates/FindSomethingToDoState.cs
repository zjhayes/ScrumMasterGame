using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSomethingToDoState : CharacterState
{
    protected ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;

        base.Handle(controller);
    }

    void Update()
    {
        // Find open task to work on.
        List<Task> assignedTasks = gameManager.Board.GetTasksWithAssignee(character);
        foreach (Task task in assignedTasks)
        {
            if (task.Status == TaskStatus.IN_PROGRESS)
            {
                ContinueInProgressTask(task);
                return;
            }
            else if (task.Status == TaskStatus.TO_DO)
            {
                // The board will determine which assigned task the player takes.
                GetTaskFromBoard();
                return;
            }
        }

        // Find another developer to pair program with.
        WorkStation pairProgrammingStation = gameManager.Interactables.FindPairProgrammingStation();
        if(pairProgrammingStation != null)
        {
            character.GoInteractWith(pairProgrammingStation);
            return;
        }

        // If good time management, go to certification station.
        if(character.Stats.TimeManagement > 2) // TODO: Determine calculation for helpful perk.
        {
            character.GoInteractWith(gameManager.Interactables.CertificationStation);
        }

        // Else dilly dally, continue looking for something to do.'
        // TODO: Add emote for procrastination.
    }

    Interactable FindSomethingToDo()
    {
        foreach (Interactable interactable in gameManager.Interactables.OpenInteractables)
        {
            return interactable; // TODO
        }
        return null; // Nothing to do.
    }

    void ContinueInProgressTask(Task task)
    {
        // If task in progress and in computer, go to station
        // If task in progress and on floor, pick up
        //character.GoInteractWith(task.Cartridge); // TODO: figure out how to get to cartridge
    }

    void GetTaskFromBoard()
    {
        // Go get task from scrum board.
        character.GoInteractWith(gameManager.Interactables.ScrumBoard);
    }

    public override string Status
    {
        get { return "Thinking"; }
    }
}
