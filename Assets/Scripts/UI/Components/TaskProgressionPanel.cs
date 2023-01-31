using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskProgressionPanel : MonoBehaviour
{
    [SerializeField]
    ProgressBar usabilityProgressBar;
    [SerializeField]
    ProgressBar stabilityProgressBar;
    [SerializeField]
    ProgressBar functionalityProgressBar;
    [SerializeField]
    ProgressBar maintainabilityProgressBar;
    [SerializeField]
    TextMeshProUGUI usabilityModifier;
    [SerializeField]
    TextMeshProUGUI stabilityModifier;
    [SerializeField]
    TextMeshProUGUI functionalityModifier;
    [SerializeField]
    TextMeshProUGUI maintainabilityModifier;
    [SerializeField]
    Color positiveColor;
    [SerializeField]
    Color negativeColor;

    public void UpdateProgression(ProductionStats stats)
    {
        usabilityProgressBar.CurrentFill = stats.Usability;
        stabilityProgressBar.CurrentFill = stats.Stability;
        functionalityProgressBar.CurrentFill = stats.Functionality;
        maintainabilityProgressBar.CurrentFill = stats.Maintainability;
    }

    public void UpdateModifiers(ProductionStats taskStats, CharacterStats assigneeStats)
    {
        UpdateModifier(usabilityModifier, CalculateModifier(taskStats.Usability, assigneeStats.Frontend));
        UpdateModifier(stabilityModifier, CalculateModifier(taskStats.Stability, assigneeStats.Backend, assigneeStats.ProblemSolving));
        UpdateModifier(functionalityModifier, CalculateModifier(taskStats.Functionality, assigneeStats.Frontend, assigneeStats.Backend));
        UpdateModifier(maintainabilityModifier, CalculateModifier(taskStats.Maintainability, assigneeStats.ProblemSolving));
    }


    public void ClearModifiers()
    {
        usabilityModifier.text = "";
        stabilityModifier.text = "";
        functionalityModifier.text = "";
        maintainabilityModifier.text = "";
    }

    void UpdateModifier(TextMeshProUGUI modifierText, int modifier)
    {
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

    int CalculateModifier(int productionStat, int characterStat1, int characterStat2 = 0)
    {
        return (characterStat1 + characterStat2) - productionStat;
    }
}
