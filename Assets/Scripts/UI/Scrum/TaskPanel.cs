using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ButtonController))]
public class TaskPanel : MenuController, IContainable
{
    [SerializeField]
    private Story story;
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

    public Story Story
    {
        get { return story; }
        set { story = value; }
    }

    public override void SetUp() // Set Story first.
    {
        button = GetComponent<ButtonController>();
        button.OnClick += Selected;
        UpdateDetails();
        UpdateTaskTypeIcon();
        UpdateAssigneePortrait();

        story.OnAssigneeChanged += OnAssigneeChanged;
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
        if (story.Assignee != null)
        {
            assigneeImage.sprite = story.Assignee.Portrait;
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
        summaryText.text = story.Details.Summary;
        storyPointsText.text = story.StoryPoints.ToString();
    }

    private void UpdateTaskTypeIcon()
    {
        taskTypeIcon.sprite = gameManager.UI.Icons.GetIconForStoryType(story.Details.Type);
    }

    private void OnDestroy()
    {
        // Clear listeners.
        onSelected = null;
        onUpdated = null;
        story.OnAssigneeChanged -= OnAssigneeChanged;
    }
}
