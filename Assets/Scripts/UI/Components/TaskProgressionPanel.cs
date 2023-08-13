using UnityEngine;
using TMPro;

public class TaskProgressionPanel : MonoBehaviour
{
    [SerializeField]
    private ProgressBar usabilityProgressBar;
    [SerializeField]
    private ProgressBar stabilityProgressBar;
    [SerializeField]
    private ProgressBar functionalityProgressBar;
    [SerializeField]
    private ProgressBar maintainabilityProgressBar;
    [SerializeField]
    private TextMeshProUGUI usabilityModifier;
    [SerializeField]
    private TextMeshProUGUI stabilityModifier;
    [SerializeField]
    private TextMeshProUGUI functionalityModifier;
    [SerializeField]
    private TextMeshProUGUI maintainabilityModifier;
    [SerializeField]
    private Color positiveColor;
    [SerializeField]
    private Color negativeColor;

    public void UpdateProgression(ProductionStats stats)
    {
        // Update fill of progress bars.
        usabilityProgressBar.CurrentFill = stats.Usability;
        stabilityProgressBar.CurrentFill = stats.Stability;
        functionalityProgressBar.CurrentFill = stats.Functionality;
        maintainabilityProgressBar.CurrentFill = stats.Maintainability;
    }

    public void UpdateModifiers(ProductionStats taskStats, CharacterStats assigneeStats)
    {
        // Update modifier text, comparing their stats.
        UpdateModifier(usabilityModifier, CalculateModifier(taskStats.Usability, assigneeStats.Frontend));
        UpdateModifier(stabilityModifier, CalculateModifier(taskStats.Stability, assigneeStats.Backend, assigneeStats.ProblemSolving));
        UpdateModifier(functionalityModifier, CalculateModifier(taskStats.Functionality, assigneeStats.Frontend, assigneeStats.Backend));
        UpdateModifier(maintainabilityModifier, CalculateModifier(taskStats.Maintainability, assigneeStats.ProblemSolving));
    }


    public void ClearModifiers()
    {
        // Set modifier text to empty string.
        usabilityModifier.text = "";
        stabilityModifier.text = "";
        functionalityModifier.text = "";
        maintainabilityModifier.text = "";
    }

    private void UpdateModifier(TextMeshProUGUI modifierText, int modifier)
    {
        // Sets modifier text so positive numbers show "+".
        if (modifier >= 0)
        {
            modifierText.text = "+" + modifier.ToString();
            modifierText.color = positiveColor;
        }
        else
        {
            modifierText.text = modifier.ToString();
            modifierText.color = negativeColor;
        }
    }

    private int CalculateModifier(int productionStat, int characterStat1, int characterStat2 = 0)
    {
        // Calculate difference of character's stats against required stats.
        return (characterStat1 + characterStat2) - productionStat;
    }
}
