using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindSomethingToDoState : PacingState
{
    public FindSomethingToDoState(ICharacterController character, IGameManager gameManager) : base(character, gameManager){}

    public override void Exit()
    {
        StopIdleEmote();
        base.Exit();
    }

    public override string Status
    {
        get { return "Need something to do..."; }
    }

    protected override void OnBegin()
    {
        FindSomethingToDo();
    }

    private void FindSomethingToDo()
    {
        // Take a given number of this character's highest priority items, the smaller the number the better the character's choice will be.
        IEnumerable<KeyValuePair<Interactable, int>> priorities = gameManager.Interactables.PrioritizeInteractablesFor(character).Take(character.Properties.NumberOfPrioritiesConsidered);
        Interactable priority = WeighPriorityDecision(priorities); // Choose randomly from high priority items, factoring in priority score.

        if (priority != null)
        {
            character.GoInteractWith(priority);
        }
        else
        {
            // Dilly dally, continue looking for something to do.
            base.Pace();
        }
    }

    // Choose from highest priority interactables, weighing their priority scores.
    private Interactable WeighPriorityDecision(IEnumerable<KeyValuePair<Interactable, int>> priorities)
    {
        Interactable priority = null;
        int cumulativeScore = priorities.Sum(pair => pair.Value);
        int randomValue = Random.Range(1, cumulativeScore + 1);
        
        int currentSum = 0;
        foreach (var pair in priorities)
        {
            currentSum += pair.Value;
            if(randomValue <= currentSum)
            {
                priority = pair.Key;
                break;
            }
        }

        return priority;
    }

    protected override void WaitAfterPacing()
    {
        StartIdleEmote();
        base.WaitAfterPacing();
    }

    protected override void AfterWait()
    {
        FindSomethingToDo();
    }

    private void StartIdleEmote()
    {
        character.Properties.IdleBubble.Show();
    }

    private void StopIdleEmote()
    {
        character.Properties.IdleBubble.Hide();
    }
}
