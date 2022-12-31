using UnityEngine;
using UnityEngine.UI;

public class CharacterCardController : MonoBehaviour
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


    public void Show(CharacterController character)
    {
        portraitPanel.sprite = character.Portrait;
        frontendProgressBar.CurrentFill = character.Stats.Frontend;
        backendProgressBar.CurrentFill = character.Stats.Backend;
        problemSolvingProgressBar.CurrentFill = character.Stats.ProblemSolving;
        timeManagementProgressBar.CurrentFill = character.Stats.TimeManagement;

        gameObject.active = true;
    }

    public void Hide()
    {
        gameObject.active = false;
    }

}
