using System.Collections.Generic;
using UnityEngine;

public class TaskComputer : Computer
{
    private List<ICharacterController> developers;
    private float proficiency = 0f; // Score based on developer(s) proficiency at current task.
    private readonly float baseSpeed = 10f;

    protected override void Awake()
    {
        base.Awake();
        developers = new List<ICharacterController>();
    }

    // Update task completeness and developer progression.
    protected override void IterateWork()
    {
        if (cartridgeReceptacle.TryGet(out Cartridge cartridge))
        {
            if (developers.Count <= 0) { return; } // No developers.

            float difficulty = (1f / cartridge.Story.StoryPoints); // Inverse of story points.

            // Progress faster with more developers and fewer story points.
            float progress = developers.Count * difficulty;
            cartridge.Story.Outcome.Completeness += progress * baseSpeed * Time.deltaTime;

            // Update chance of errors based on developer proficiency compared to task difficulty.
            float outcome = proficiency * difficulty;
            cartridge.Story.Outcome.ChanceOfErrors -= outcome * Time.deltaTime;
            
            if (cartridge.Story.Outcome.IsReadyForProduction)
            {
                // Work is complete.
                Sleep();
            }
        }
        else
        {
            Sleep(); // No cartridge.
        }
    }

    public void SignInDeveloper(ICharacterController developer)
    {
        developers.Add(developer);
        UpdateProficiency();
    }

    public void SignOutDeveloper(ICharacterController developer)
    {
        developers.Remove(developer);
        UpdateProficiency();
    }

    private void UpdateProficiency()
    {
        // Compare developer(s) stats to current task requirements and score proficiency.
        if (cartridgeReceptacle.TryGet(out Cartridge cartridge))
        {
            proficiency = WorkCalculator.CalculateCombinedOutcome(cartridge.Story.Details.Requirements, developers);
        }
    }
}
