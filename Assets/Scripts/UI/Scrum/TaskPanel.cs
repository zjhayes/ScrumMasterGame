using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ButtonController))]
public class TaskPanel : MenuController, IContainable
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
    public event OnSelected onSelected;

    public delegate void OnUpdated(TaskPanel taskPanel);
    public event OnUpdated onUpdated;

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

    public override void Show()
    {
        base.Show();
        SetActive(true);
    }

    public override void Hide()
    {
        base.Hide();
        SetActive(false);
    }

    void Start()
    {
        UpdateDetails();
        UpdateTaskTypeIcon();
        UpdateAssigneePortrait();

        task.onAssigneeChanged += OnAssigneeChanged;
    }
    
    void Selected()
    {
        onSelected?.Invoke(this);
    }
    
    void OnAssigneeChanged()
    {
        UpdateAssigneePortrait();
        onUpdated?.Invoke(this);
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

    void OnDestroy()
    {
        // Clear listeners.
        onSelected = null;
        onUpdated = null;
        task.onAssigneeChanged -= OnAssigneeChanged;
    }
}
