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

            // Progress faster with more developers, fewer story points, and a more maintainable production.
            float difficulty = (1f / cartridge.Story.StoryPoints); // Inverse of story points.
            float progress = CalculateDeveloperProductivity() * difficulty;
            cartridge.Story.Outcome.Completeness += progress * Time.deltaTime;

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

    private float CalculateDeveloperProductivity()
    {
        float developmentSpeed = developers.Count * baseSpeed;
        float maintainability = CalculateMaintainabilityBuff();
        return developmentSpeed * maintainability;
    }

    private float CalculateMaintainabilityBuff()
    {
        // Maintainability increases development by up to double.
        return (gameManager.Production.Stats.Maintainability / gameManager.Production.Stats.Maximum) + Numeric.ONE;
    }
}
