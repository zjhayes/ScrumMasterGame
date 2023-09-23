using UnityEngine;

public class WorkCalculator
{
    public static float CalculateModifier(int productionStat, int characterStat1, int characterStat2 = 0)
    {
        // Calculate difference of character's stats against required stats.
        return (characterStat1 + characterStat2) - productionStat;
    }
}
