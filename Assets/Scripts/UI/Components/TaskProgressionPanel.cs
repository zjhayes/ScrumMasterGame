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
        UpdateModifier(usabilityModifier, (int) WorkCalculator.CalculateUsabilityOutcome(taskStats, assigneeStats));
        UpdateModifier(stabilityModifier, (int) WorkCalculator.CalculateStabilityOutcome(taskStats, assigneeStats));
        UpdateModifier(functionalityModifier, (int) WorkCalculator.CalculateFunctionalityOutcome(taskStats, assigneeStats));
        UpdateModifier(maintainabilityModifier, (int) WorkCalculator.CalculateMaintainabilityOutcome(taskStats, assigneeStats));
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
}
