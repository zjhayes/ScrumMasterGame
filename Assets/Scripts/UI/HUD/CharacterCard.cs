using UnityEngine;
using UnityEngine.UI;

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
    StatusBar statusBar;

    public override void SetUp()
    {
        base.Hide();
        base.SetUp();
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
    }
}