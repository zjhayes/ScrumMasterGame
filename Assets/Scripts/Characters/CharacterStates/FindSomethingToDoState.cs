using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindSomethingToDoState : CharacterState
{
    [SerializeField]
    private int numberOfPrioritiesConsidered = 3; // Increasing this number makes the character's actions more random.
    [SerializeField]
    private OverheadElement idleBubble;
    [SerializeField]
    private Boundary paceBoundary;
    [SerializeField]
    private float paceSpeed = 0.5f;
    [SerializeField]
    private float minWaitTime = 2.0f;
    [SerializeField]
    private float maxWaitTime = 10.0f;

    protected ICharacterController character;
    private Coroutine waitAndMoveAction;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        base.Handle(controller);
    }

    public override void Exit()
    {
        StopIdleEmote();
        CancelWaitAndMove();
        base.Exit();
    }

    public override string Status
    {
        get { return "Dilly-Dallying"; }
    }

    private void Update()
    {
        if(IsPacing()) { return; } // Do nothing while pacing.

        // Determine this character's current priority.
        Interactable priority = FindSomethingToDo();

        if (priority != null)
        {
            StopIdleEmote();
            character.GoInteractWith(priority);
        }
        else 
        {
            // Dilly dally, continue looking for something to do.
            Pace();
        }
    }

    private Interactable FindSomethingToDo()
    {
        // Get scores advertised to character by open interactables.
        Dictionary<Interactable, int> advertisements = new Dictionary<Interactable, int>();
        foreach (Interactable interactable in gameManager.Interactables.OpenInteractables)
        {
            int priorityScore = interactable.CalculatePriorityFor(character);
            advertisements.Add(interactable, priorityScore);
        }

        // Sort positive interactable advertisements by score, take only a given number of the higher scored advertisements.
        IEnumerable<KeyValuePair<Interactable,int>> priorities = advertisements.Where(pair => pair.Value > 0).OrderByDescending(pair => pair.Value).Take(numberOfPrioritiesConsidered);

        // Weigh the highest scores and choose at random.
        return WeighPriorityDecision(priorities);
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

    private void Pace()
    {
        // Pace around randomly.
        if (character.Movement.IsStopped() && !IsPacing())
        {
            waitAndMoveAction = StartCoroutine(WaitAndMove());
        }
    }

    private IEnumerator WaitAndMove()
    {
        float delayTime = Random.Range(minWaitTime, maxWaitTime);

        yield return new WaitForSeconds(delayTime);

        character.Movement.GoToBoundary(paceBoundary, character.Movement.BaseSpeed * paceSpeed);
        waitAndMoveAction = null;
        StartIdleEmote();
    }

    private void CancelWaitAndMove()
    {
        if (waitAndMoveAction != null)
        {
            StopCoroutine(waitAndMoveAction);
            waitAndMoveAction = null;
        }
    }

    private void StartIdleEmote()
    {
        idleBubble.Show();
    }

    private void StopIdleEmote()
    {
        idleBubble.Hide();
    }

    private bool IsPacing()
    {
        return waitAndMoveAction != null;
    }
}
