using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ButtonController))]
public class TaskPanel : MonoBehaviour, IContainable
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

    public delegate void OnSelected(TaskPanel taskPanel);
    public OnSelected onSelected;

    public Task Task
    {
        get { return task; }
        set { task = value; }
    }

    void Awake()
    {
        button = GetComponent<ButtonController>();
        button.onClick += Selected;
    }

    void Start()
    {
        UpdateDetails();
        UpdateTaskTypeIcon();
        UpdateAssigneePortrait();

        task.onAssigneeChanged += UpdateAssigneePortrait;
    }

    void Selected()
    {
        onSelected?.Invoke(this);
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

    public ButtonController Button
    {
        get { return button; }
    }
}
