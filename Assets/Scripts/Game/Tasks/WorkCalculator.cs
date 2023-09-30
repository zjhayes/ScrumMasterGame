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
        float usability = CalculateModifier(requirementStats.Usability, characterStats.Frontend, characterStats.ProblemSolving);
        float stability = CalculateModifier(requirementStats.Stability, characterStats.Backend, characterStats.ProblemSolving);
        float functionality = CalculateModifier(requirementStats.Functionality, characterStats.Backend, characterStats.Frontend);
        float maintainability = CalculateModifier(requirementStats.Maintainability, characterStats.TimeManagement);

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
        return CalculateModifier(requirementStats.Usability, characterStats.Frontend, characterStats.ProblemSolving);
    }

    public static float CalculateStabilityOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        return CalculateModifier(requirementStats.Stability, characterStats.Backend, characterStats.ProblemSolving);
    }

    public static float CalculateFunctionalityOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        return CalculateModifier(requirementStats.Functionality, characterStats.Backend, characterStats.Frontend);
    }

    public static float CalculateMaintainabilityOutcome(ProductionStats requirementStats, CharacterStats characterStats)
    {
        return CalculateModifier(requirementStats.Maintainability, characterStats.TimeManagement);
    }
}

/*
    Frontend = Usability and Functionality
    Backend = Stability and Functionality
    Problem Solving = Stability and Usability
    Time Management = Maintainability

    Usability = Frontend and Problem Solving
    Stability = Backend and Problem Solving
    Functionality = Frontend + Backend
    Maintainability = Time Management
*/