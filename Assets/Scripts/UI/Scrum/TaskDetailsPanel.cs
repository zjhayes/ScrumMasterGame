using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskDetailsPanel : MonoBehaviour
{
    [SerializeField]
    Image taskTypeIcon;
    [SerializeField]
    TextMeshProUGUI summaryText;
    [SerializeField]
    TextMeshProUGUI descriptionText;
    [SerializeField]
    ProgressBar usabilityProgressBar;
    [SerializeField]
    ProgressBar stabilityProgressBar;
    [SerializeField]
    ProgressBar functionalityProgressBar;
    [SerializeField]
    ProgressBar maintainabilityProgressBar;


    public void Show(Task task)
    {
        UpdateDetails(task);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateDetails(Task task)
    {
        taskTypeIcon.sprite = task.TaskTypeIcon;
        summaryText.text = task.Summary;
        descriptionText.text = task.Description;
        usabilityProgressBar.CurrentFill = task.Stats.Usability;
        stabilityProgressBar.CurrentFill = task.Stats.Stability;
        functionalityProgressBar.CurrentFill = task.Stats.Functionality;
        maintainabilityProgressBar.CurrentFill = task.Stats.Maintainability;
    }
}
