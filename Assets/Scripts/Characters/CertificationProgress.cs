using UnityEngine;

public class CertificationProgress
{
    private CharacterStats characterStats;
    private CharacterStats progress;

    public CertificationProgress(CharacterStats stats)
    {
        characterStats = stats;
        progress = new CharacterStats();
        progress.OnStatUpdated += OnProgressStatUpdated;
    }

    public void AddProgress(CharacterStats newProgress)
    {
        progress.Add(newProgress);
        ApplyProgress();
    }

    public CharacterStats Progress
    {
        get { return progress; }
    }

    private void ApplyProgress()
    {
        // Check each stat and apply a level-up if progress reaches 10.
        if (progress.Frontend >= CharacterStats.MAXIMUM)
        {
            characterStats.Frontend += 1;
            progress.Frontend = 0; // Reset progress after leveling up.
        }

        if (progress.Backend >= CharacterStats.MAXIMUM)
        {
            characterStats.Backend += 1;
            progress.Backend = 0;
        }

        if (progress.ProblemSolving >= CharacterStats.MAXIMUM)
        {
            characterStats.ProblemSolving += 1;
            progress.ProblemSolving = 0;
        }

        if (progress.TimeManagement >= CharacterStats.MAXIMUM)
        {
            characterStats.TimeManagement += 1;
            progress.TimeManagement = 0;
        }
    }

    private void OnProgressStatUpdated(CharacterStat stat)
    {
        ApplyProgress();
    }
}