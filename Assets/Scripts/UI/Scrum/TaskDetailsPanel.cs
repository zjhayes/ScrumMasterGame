using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskDetailsPanel : MenuController
{
    [SerializeField]
    Image taskTypeIcon;
    [SerializeField]
    TextMeshProUGUI summaryText;
    [SerializeField]
    TextMeshProUGUI descriptionText;
    [SerializeField]
    TextMeshProUGUI storyPointsText;
    [SerializeField]
    TMP_Dropdown assigneeSelection;
    [SerializeField]
    TaskProgressionPanel taskProgressionPanel;

    Story story;
    Dictionary<int, ICharacterController> characterCache;

    public override void SetUp()
    {
        // Set up assignee selection.
        if (characterCache == null || characterCache.Count <= 0)
        {
            AddCharactersToAssigneeOptions();
        }
    }

    public void Show(Story story)
    {
        this.story = story;
        UpdateDetails();
        UpdateAssignee();

        Show();
    }

    public override void Show()
    {
        base.Show();
        SetActive(true);
    }

    public override void Hide()
    {
        base.Hide();
        story = null;
        SetActive(false);
    }

    public override void Escape()
    {
        Hide();
    }

    // Set assignee field event to call OnAssigneeSelected.
    public void OnAssigneeSelected()
    { 
        if(characterCache.ContainsKey(assigneeSelection.value))
        {
            // Set task assignee to selected character.
            story.Assignee = characterCache[assigneeSelection.value];
        }
        else
        {
            // Task has been unassigned.
            story.Assignee = null;
        }
        taskProgressionPanel.ClearModifiers();
    }

    public void UpdateAssignee()
    {
        // Find task assignee's option index in character cache.
        int assigneeKey = characterCache.FirstOrDefault(x => x.Value == story.Assignee).Key;
        assigneeSelection.value = assigneeKey;
    }

    public void UpdateDetails()
    {
        taskTypeIcon.sprite = gameManager.UI.Icons.GetIconForStoryType(story.Details.Type);
        summaryText.text = story.Details.Summary;
        descriptionText.text = story.Details.Description;
        storyPointsText.text = story.StoryPoints.ToString();
        taskProgressionPanel.UpdateProgression(story.Details.Requirements);
        ClearModifiers();
    }

    public void UpdateModifiers(int assigneeIndex)
    {
        if(characterCache.ContainsKey(assigneeIndex))
        {
            ICharacterController assignee = characterCache[assigneeIndex];
            taskProgressionPanel.UpdateModifiers(story.Details.Requirements, assignee.Stats);
        }
        else
        {
            ClearModifiers();
        }
    }

    public void ClearModifiers()
    {
        taskProgressionPanel.ClearModifiers();
    }

    private void AddCharactersToAssigneeOptions()
    {
        characterCache = new Dictionary<int, ICharacterController>();
        List<Sprite> portraits = new List<Sprite>();
        int index = 1; // 0 Represents unassigned.
        foreach (ICharacterController character in gameManager.Team.Characters)
        {
            characterCache.Add(index++, character);
            portraits.Add(character.Portrait);
        }
        assigneeSelection.AddOptions(portraits);
    }

    public Story Story
    {
        get { return story; }
    }
}
