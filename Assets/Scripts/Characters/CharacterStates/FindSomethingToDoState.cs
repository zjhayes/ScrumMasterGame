using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindSomethingToDoState : CharacterState
{
    [SerializeField]
    private int numberOfPrioritiesConsidered = 3; // Increasing this number makes the character's actions more random.

    protected ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;

        base.Handle(controller);
    }

    void Update()
    {
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
            StartIdleEmote();
        }
    }

    Interactable FindSomethingToDo()
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
        
        if(priorities.Any())
        {
            // Weigh the highest scores and choose at random.
            return WeighPriorityDecision(priorities);
        }
        else
        {
            return null; // Nothing to do.
        }
    }

    void StartIdleEmote()
    {
        if(!character.OverHead.HasSpeechBubble())
        {
            character.OverHead.ShowIdleBubble();
        }
    }

    void StopIdleEmote()
    {
        character.OverHead.HideIdleBubble();
    }

    // Choose from priorities, weighing their priority scores.
    Interactable WeighPriorityDecision(IEnumerable<KeyValuePair<Interactable, int>> priorities)
    {
        int cumulativeScore = priorities.Sum(pair => pair.Value);
        int randomValue = Random.Range(1, cumulativeScore + 1);
        
        int currentSum = 0;
        foreach (var pair in priorities)
        {
            currentSum += pair.Value;
            if(randomValue <= currentSum)
            {
                return pair.Key;
            }
        }

        Debug.Log("Unable to find something to do.");
        return null;
    }

    public override void Exit()
    {
        StopIdleEmote();
        base.Exit();
    }

    public override string Status
    {
        get { return "Thinking"; }
    }
}
