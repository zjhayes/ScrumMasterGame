using System.Collections.Generic;
using UnityEngine;

public class TaskComputer : Computer
{
    private List<ICharacterController> developers;

    protected override void Awake()
    {
        base.Awake();
        developers = new List<ICharacterController>();
    }

    // Update task completeness and developer progression.
    protected override void IterateWork()
    {
        if (cartridgeReceptacle.TryGetPickup(out Cartridge cartridge))
        {
            if (developers.Count <= 0) { return; } // No developers.
            
            cartridge.Task.Completeness += .1f * developers.Count;
            cartridge.Task.Outcome.ChanceOfErrors -= CalculateOutcome(cartridge);

            if (cartridge.Task.IsReadyForProduction)
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

    private float CalculateOutcome(Cartridge cartridge)
    {
        float outcome = 0;

        foreach (ICharacterController developer in developers)
        {
            float usability = CalculateModifier(cartridge.Task.Stats.Usability, developer.Stats.Frontend);
            float stability = CalculateModifier(cartridge.Task.Stats.Stability, developer.Stats.Backend, developer.Stats.ProblemSolving);
            float functionality = CalculateModifier(cartridge.Task.Stats.Functionality, developer.Stats.Backend, developer.Stats.Frontend);
            float maintainability = CalculateModifier(cartridge.Task.Stats.Maintainability, developer.Stats.TimeManagement, developer.Stats.ProblemSolving);
            outcome = usability + stability + functionality + maintainability;
        }

        return outcome;
    }

    private float CalculateModifier(int productionStat, int characterStat1, int characterStat2 = 0)
    {
        // Calculate difference of character's stats against required stats.
        return (characterStat1 + characterStat2) - productionStat;
    }

    public void SignInDeveloper(ICharacterController developer)
    {
        developers.Add(developer);
    }

    public void SignOutDeveloper(ICharacterController developer)
    {
        developers.Remove(developer);
    }
}
