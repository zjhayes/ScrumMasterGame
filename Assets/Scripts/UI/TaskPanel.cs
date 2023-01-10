using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskPanel : MonoBehaviour
{
    [SerializeField]
    private Task task;
    [SerializeField]
    Image taskTypeIcon;
    [SerializeField]
    TextMeshProUGUI summaryText;
    [SerializeField]
    Image assigneeImage;
    [SerializeField]
    Sprite defaultAssigneePortrait;
    [SerializeField]
    TextMeshProUGUI storyPointsText;

    public Task Task
    {
        get { return task; }
        set { task = value; }
    }

    void Start()
    {
        UpdateDetails();

    }

    void UpdateDetails()
    {
        taskTypeIcon.sprite = task.TaskTypeIcon;
        summaryText.text = task.Summary;
        storyPointsText.text = task.StoryPoints.ToString();

        if(task.Assignee)
        {
            assigneeImage.sprite = task.Assignee?.Portrait;
        }
        else
        {
            assigneeImage.sprite = defaultAssigneePortrait;
        }
    }
}
