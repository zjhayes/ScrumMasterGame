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
        List<Task> assignedTasks = gameManager.Sprint.Board.GetTasksWithAssignee(character);
        // If task in progress and in computer, go to station
        // If task in progress and on floor, pick up
        // If assigned task in to do, go to scrum board.
        // Dilly dally
        // If someone working, paired program
        // If good time management, go to certification station
    }

    public override string Status
    {
        get { return "Thinking"; }
    }
}
