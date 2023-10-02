using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ButtonController))]
public class TaskPanel : MenuController, IContainable
{
    [SerializeField]
    private Task task;
    [SerializeField]
    private Image taskTypeIcon;
    [SerializeField]
    private TextMeshProUGUI summaryText;
    [SerializeField]
    private Image assigneeImage;
    [SerializeField]
    private Sprite defaultAssigneePortrait;
    [SerializeField]
    private TextMeshProUGUI storyPointsText;

    private ButtonController button;

    public delegate void OnSelected(TaskPanel taskPanel);
    public event OnSelected onSelected;

    public delegate void OnUpdated(TaskPanel taskPanel);
    public event OnUpdated onUpdated;

    public Task Task
    {
        get { return task; }
        set { task = value; }
    }
    private void Awake()
    {
        // Set up self on creation.
        SetUp();
    }

    private void Start()
    {
        UpdateDetails();
        UpdateTaskTypeIcon();
        UpdateAssigneePortrait();

        task.OnAssigneeChanged += OnAssigneeChanged;
    }

    public override void SetUp()
    {
        button = GetComponent<ButtonController>();
        button.OnClick += Selected;
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

    public void UpdateAssigneePortrait()
    {
        if (task.Assignee != null)
        {
            assigneeImage.sprite = task.Assignee.Portrait;
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

    private void Selected()
    {
        onSelected?.Invoke(this);
    }

    private void OnAssigneeChanged(ICharacterController assignee)
    {
        UpdateAssigneePortrait();
        onUpdated?.Invoke(this);
    }

    private void UpdateDetails()
    {
        summaryText.text = task.Summary;
        storyPointsText.text = task.StoryPoints.ToString();
    }

    private void UpdateTaskTypeIcon()
    {
        taskTypeIcon.sprite = task.TaskTypeIcon;
    }

    private void OnDestroy()
    {
        // Clear listeners.
        onSelected = null;
        onUpdated = null;
        task.OnAssigneeChanged -= OnAssigneeChanged;
    }
}
