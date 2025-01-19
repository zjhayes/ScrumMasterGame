using System.Collections.Generic;
using UnityEngine;

public class TaskComputer : Computer
{
    private List<ICharacterController> developers;
    private float proficiency = 0f; // Score based on developer(s) proficiency at current task.
    private readonly float BASE_SPEED = 10f;

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
            float progress = CalculateDeveloperProductivity() * (1f / cartridge.Story.Details.StoryPoints);
            cartridge.Story.Outcome.Completeness += progress * Time.deltaTime;

            // Update chance of errors based on developer proficiency.
            cartridge.Story.Outcome.ChanceOfErrors -= proficiency * progress * Time.deltaTime;

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
            proficiency = WorkCalculator.CalculateProficiency(cartridge.Story.Details.Requirements, developers);
        }
    }

    private float CalculateDeveloperProductivity()
    {
        float developmentSpeed = developers.Count * BASE_SPEED;
        float maintainability = CalculateMaintainabilityBuff();
        return developmentSpeed * maintainability;
    }

    private float CalculateMaintainabilityBuff()
    {
        // Maintainability increases development by 1 to 10.
        return (gameManager.Production.Stats.Maintainability / (float) gameManager.Production.Stats.Maximum) * 9f + Numeric.ONE;
    }
}
