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
    TextMeshProUGUI qualityText;
    [SerializeField]
    TextMeshProUGUI defectRateText;
    [SerializeField]
    TextMeshProUGUI cycleTimeText;
    [SerializeField]
    TextMeshProUGUI remainingTimeText;
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
        // Update Retrospective View with sprint outcomes.
        sprintNumberText.text = gameManager.Sprint.Current.Number.ToString();
        List<Story> completeTasks = gameManager.Sprint.Current.CompleteTasks;
        List<Story> incompleteTasks = gameManager.Sprint.Current.IncompleteTasks;
        completedStoryPointsText.text = StoryService.CountStoryPoints(completeTasks).ToString();
        notCompletedStoryPointsText.text = StoryService.CountStoryPoints(incompleteTasks).ToString();
        qualityText.text = gameManager.Sprint.Current.Quality.ToString("F0");
        defectRateText.text = gameManager.Sprint.Current.Defects.Count.ToString();
        cycleTimeText.text = gameManager.Sprint.Current.CycleTime.ToString("F0");
        remainingTimeText.text = gameManager.Sprint.Current.RemainingTime.ToString("F0");
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
