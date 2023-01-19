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


    public void Show(CharacterController character)
    {
        UpdatePortrait(character);
        UpdateStatus(character);
        UpdateProgress(character);

        base.Show();
    }

    public void UpdatePortrait(CharacterController character)
    {
        portraitPanel.sprite = character.Portrait;
    }

    public void UpdateStatus(CharacterController character)
    {
        statusBar.UpdateStatusText(character.State.Status);
    }

    public void UpdateProgress(CharacterController character)
    {
        frontendProgressBar.CurrentFill = character.Stats.Frontend;
        backendProgressBar.CurrentFill = character.Stats.Backend;
        problemSolvingProgressBar.CurrentFill = character.Stats.ProblemSolving;
        timeManagementProgressBar.CurrentFill = character.Stats.TimeManagement;
    }
}
