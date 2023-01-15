using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ButtonController))]
public class TaskPanel : PanelController, IContainable
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

    ButtonController button;

    public Task Task
    {
        get { return task; }
        set { task = value; }
    }

    void Awake()
    {
        button = GetComponent<ButtonController>();
    }

    void Start()
    {
        UpdateDetails();
        UpdateTaskTypeIcon();
        UpdateAssigneePortrait();

        task.onAssigneeChanged += UpdateAssigneePortrait;
        button.onClick += OnSelect;
    }

    void OnSelect()
    {
        UIManager.Instance.PlanningWindow.ShowTaskDetails(task);
    }

    void UpdateDetails()
    {
        summaryText.text = task.Summary;
        storyPointsText.text = task.StoryPoints.ToString();
    }

    void UpdateTaskTypeIcon()
    {
        taskTypeIcon.sprite = task.TaskTypeIcon;
    }

    public void UpdateAssigneePortrait()
    {
        if (task.Assignee)
        {
            assigneeImage.sprite = task.Assignee?.Portrait;
        }
        else
        {
            assigneeImage.sprite = defaultAssigneePortrait;
        }
    }
}
