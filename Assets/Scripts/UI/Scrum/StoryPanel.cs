using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(ButtonController))]
public class StoryPanel : MenuController, IContainable
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

    public event Events.MenuEvent<StoryPanel> OnSelected;

    public event Events.MenuEvent<StoryPanel> OnAssigneeUpdated;
    public event Events.MenuEvent<StoryPanel> OnStatusUpdated;

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
        UpdateStoryTypeIcon();
        UpdateAssigneePortrait();

        story.OnAssigneeChanged += AssigneeUpdated;
        story.OnStatusChanged += StatusUpdated;
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

    public ButtonController Button
    {
        get { return button; }
    }

    private void Selected()
    {
        OnSelected?.Invoke(this);
    }

    private void AssigneeUpdated(ICharacterController assignee)
    {
        UpdateAssigneePortrait();
        OnAssigneeUpdated?.Invoke(this);
    }

    private void StatusUpdated(Story story)
    {
        OnStatusUpdated?.Invoke(this);
    }

    private void UpdateDetails()
    {
        summaryText.text = story.Details.Summary;
        storyPointsText.text = story.Details.StoryPoints.ToString();
    }

    private void UpdateStoryTypeIcon()
    {
        taskTypeIcon.sprite = gameManager.UI.Icons.GetIconForStoryType(story.Details.Type);
    }

    private void UpdateAssigneePortrait()
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

    private void OnDestroy()
    {
        story.OnAssigneeChanged -= AssigneeUpdated;
        story.OnStatusChanged -= StatusUpdated;
    }
}
