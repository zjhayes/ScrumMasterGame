using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCard : MenuController
{
    [SerializeField]
    Image portraitPanel;
    [SerializeField]
    ProgressBar frontendProgressBar;
    [SerializeField]
    ProgressBar backendProgressBar;
    [SerializeField]
    ProgressBar problemSolvingProgressBar;
    [SerializeField]
    ProgressBar timeManagementProgressBar;
    [SerializeField]
    TextMeshProUGUI capacityText;
    [SerializeField]
    StatusBar statusBar;

    public override void SetUp()
    {
        base.Hide();
    }

    public void UpdateCard(ICharacterController character)
    {
        UpdatePortrait(character);
        UpdateStatus(character);
        UpdateProgress(character);
    }

    public void UpdatePortrait(ICharacterController character)
    {
        portraitPanel.sprite = character.Portrait;
    }

    public void UpdateStatus(ICharacterController character)
    {
        statusBar.UpdateStatusText(character.State.Status);
    }

    public void UpdateProgress(ICharacterController character)
    {
        frontendProgressBar.CurrentFill = character.Stats.Frontend;
        backendProgressBar.CurrentFill = character.Stats.Backend;
        problemSolvingProgressBar.CurrentFill = character.Stats.ProblemSolving;
        timeManagementProgressBar.CurrentFill = character.Stats.TimeManagement;

        // Count story points assigned to character.
        int assignedStoryPoints = StoryService.CountStoryPoints(gameManager.Board.Stories.AssignedTo(character).Get());
        capacityText.text = $"{assignedStoryPoints}/{character.Stats.Velocity}";
    }
}