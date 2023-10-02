using System.Collections.Generic;
using UnityEngine;

public class WorkCalculator
{
    public static float CalculateModifier(float productionStat, float characterStat1, float characterStat2 = 0)
    {
        // Calculate difference of character's stats against required stats.
        return (characterStat1 + characterStat2) - productionStat;
    }

    public static float CalculateOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        // Compare difference between production stats to character stats.
        float usability = CalculateUsabilityOutcome(requirementStats, characterStats);
        float stability = CalculateStabilityOutcome(requirementStats, characterStats);
        float functionality = CalculateFunctionalityOutcome(requirementStats, characterStats);
        float maintainability = CalculateMaintainabilityOutcome(requirementStats, characterStats);

        // Return sum of stats.
        return usability + stability + functionality + maintainability;
    }

    public static float CalculateCombinedOutcome(ProductionStats requirementStats, List<ICharacterController> developers)
    {
        float outcome = 0;
        // Accumulate developer contributions to outcome.
        foreach (ICharacterController developer in developers)
        {
            // Calculate score by comparing developer stats to requirements.
            outcome += CalculateOutcome(requirementStats, developer.Stats);
        }

        return outcome;
    }

    public static float CalculateUsabilityOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        return CalculateModifier(requirementStats.Usability, characterStats.Frontend, characterStats.TimeManagement);
    }

    public static float CalculateStabilityOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        return CalculateModifier(requirementStats.Stability, characterStats.Backend, characterStats.TimeManagement);
    }

    public static float CalculateFunctionalityOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        return CalculateModifier(requirementStats.Functionality, characterStats.Frontend, characterStats.ProblemSolving);
    }

    public static float CalculateMaintainabilityOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        return CalculateModifier(requirementStats.Maintainability, characterStats.Backend, characterStats.ProblemSolving);
    }
}

/*
    Frontend = Usability and Functionality
    Backend = Stability and Maintainability
    Problem Solving = Maintainability and Functionality
    Time Management = Usability and Stability +Increased speed and efficiency.

    Usability = Frontend and Time Management
    Stability = Backend and Time Management
    Functionality = Frontend and Problem Solving
    Maintainability = Backend and Problem Solving
*/