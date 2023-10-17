using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RetrospectiveMenuController : MenuController
{
    [SerializeField]
    private TextMeshProUGUI sprintNumberText;
    [SerializeField]
    private TextMeshProUGUI completedStoryPointsText;
    [SerializeField]
    private TextMeshProUGUI notCompletedStoryPointsText;
    [SerializeField]
    private TextMeshProUGUI qualityText;
    [SerializeField]
    private TextMeshProUGUI defectRateText;
    [SerializeField]
    private TextMeshProUGUI cycleTimeText;
    [SerializeField]
    private TextMeshProUGUI userCountText;
    [SerializeField]
    private TextMeshProUGUI userCountModifierText;
    [SerializeField]
    TextMeshProUGUI remainingTimeText;
    [SerializeField]
    private ProgressBar usabilityBar;
    [SerializeField]
    private ProgressBar stabilityBar;
    [SerializeField]
    private ProgressBar functionalityBar;
    [SerializeField]
    private ProgressBar maintainabilityBar;
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
        SetSprintDetails();
        SetProductionDetails();
    }

    private void SetSprintDetails()
    {
        sprintNumberText.text = gameManager.Sprint.Current.Number.ToString();
        List<Story> completeTasks = gameManager.Sprint.Current.CompleteTasks;
        List<Story> incompleteTasks = gameManager.Sprint.Current.IncompleteTasks;
        completedStoryPointsText.text = StoryService.CountStoryPoints(completeTasks).ToString();
        notCompletedStoryPointsText.text = StoryService.CountStoryPoints(incompleteTasks).ToString();
        qualityText.text = gameManager.Sprint.Current.Quality.ToString("F0");
        defectRateText.text = gameManager.Sprint.Current.Defects.Count.ToString();
        cycleTimeText.text = gameManager.Sprint.Current.CycleTime.ToString("F0");
        remainingTimeText.text = gameManager.Sprint.Current.RemainingTime.ToString("F0");
    }

    private void SetProductionDetails()
    {
        // Update user count.
        userCountText.text = gameManager.Production.UserCount.ToString();
        userCountModifierText.text = $"(+{gameManager.Sprint.Current.NewUserCount})";

        // Update progress bars with production stat values and maximum.
        usabilityBar.Maximum = gameManager.Production.Stats.Maximum;
        usabilityBar.CurrentFill = gameManager.Production.Stats.Usability;
        stabilityBar.Maximum = gameManager.Production.Stats.Maximum;
        stabilityBar.CurrentFill = gameManager.Production.Stats.Stability;
        functionalityBar.Maximum = gameManager.Production.Stats.Maximum;
        functionalityBar.CurrentFill = gameManager.Production.Stats.Functionality;
        maintainabilityBar.Maximum = gameManager.Production.Stats.Maximum;
        maintainabilityBar.CurrentFill = gameManager.Production.Stats.Maintainability;
    }

    private void ContinueToNextSprint()
    {
        gameManager.Sprint.BeginPlanning();
    }
}
