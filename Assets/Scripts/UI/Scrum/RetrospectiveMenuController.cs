using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RetrospectiveMenuController : MenuController
{
    [SerializeField]
    TextMeshProUGUI sprintNumberText;
    [SerializeField]
    TextMeshProUGUI completedStoryPointsText;
    [SerializeField]
    TextMeshProUGUI notCompletedStoryPointsText;
    [SerializeField]
    TextMeshProUGUI proficiencyText;
    [SerializeField]
    ProgressBar usabilityBar;
    [SerializeField]
    ProgressBar stabilityBar;
    [SerializeField]
    ProgressBar functionalityBar;
    [SerializeField]
    ProgressBar maintainabilityBar;
    [SerializeField]
    private ButtonController nextSprintButton;

    public override void SetUp()
    {
        nextSprintButton.OnClick += ContinueToNextSprint;
    }

    public override void Show()
    {
        LoadDetails();
        base.Show();
    }

    private void LoadDetails()
    {
        sprintNumberText.text = gameManager.Sprint.Current.Number.ToString();
        List<Task> completeTasks = gameManager.Sprint.Current.CompleteTasks;
        List<Task> incompleteTasks = gameManager.Sprint.Current.IncompleteTasks;
        completedStoryPointsText.text = gameManager.Board.CountStoryPoints(completeTasks).ToString();
        notCompletedStoryPointsText.text = gameManager.Board.CountStoryPoints(incompleteTasks).ToString();
        proficiencyText.text = gameManager.Sprint.Current.Proficiency.ToString("F0");
        SetProductionProgressBars();
    }

    private void SetProductionProgressBars()
    {
        usabilityBar.CurrentFill = gameManager.Production.Stats.Usability;
        stabilityBar.CurrentFill = gameManager.Production.Stats.Stability;
        functionalityBar.CurrentFill = gameManager.Production.Stats.Functionality;
        maintainabilityBar.CurrentFill = gameManager.Production.Stats.Maintainability;
    }

    private void ContinueToNextSprint()
    {
        gameManager.Sprint.BeginPlanning();
    }
}
