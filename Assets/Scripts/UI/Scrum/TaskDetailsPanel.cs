using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//* DEPRECATED *//
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
    [SerializeField]
    Button addToSprintButton;
    [SerializeField]
    Button removeFromSprintButton;

    Task task;
    Dictionary<int, CharacterController> characterCache;

    public delegate void OnAddToSprint(Task task);
    public OnAddToSprint onAddToSprint;

    public delegate void OnRemoveFromSprint(Task task);
    public OnRemoveFromSprint onRemoveFromSprint;

    public override void SetUp()
    {
        // Set up assignee selection.
        if (characterCache == null || characterCache.Count <= 0)
        {
            AddCharactersToAssigneeOptions();
        }

        // Listen to assignee dropdown.
        assigneeSelection.onValueChanged.AddListener(delegate { OnAssigneeSelected(); });

        base.SetUp();
    }

    public override void Show()
    {
        base.Show();
        SetActive(true);
        UpdateDetails();
        UpdateAssignee();
        UpdateActionButton();
    }

    public override void Hide()
    {
        base.Hide();
        task = null;
        SetActive(false);
    }

    public override void Escape()
    {
        Hide();
    }

    public void OnAssigneeSelected()
    { 
        if(characterCache.ContainsKey(assigneeSelection.value))
        {
            // Set task assignee to selected character.
            task.Assignee = characterCache[assigneeSelection.value];
        }
        else
        {
            // Task has been unassigned.
            task.Assignee = null;
        }
        taskProgressionPanel.ClearModifiers();
        UpdateActionButton();
    }

    public void AddToSprint()
    {
        task.Status = TaskStatus.TO_DO;
        onAddToSprint?.Invoke(task);
    }

    public void RemoveFromSprint()
    {
        task.Status = TaskStatus.BACKLOG;
        onRemoveFromSprint?.Invoke(task);
    }

    public void UpdateAssignee()
    {
        // Find task assignee's option index in character cache.
        int assigneeKey = characterCache.FirstOrDefault(x => x.Value == task.Assignee).Key;
        assigneeSelection.value = assigneeKey;
        UpdateActionButton();
    }

    public void UpdateDetails()
    {
        taskTypeIcon.sprite = task.TaskTypeIcon;
        summaryText.text = task.Summary;
        descriptionText.text = task.Description;
        storyPointsText.text = task.StoryPoints.ToString();
        taskProgressionPanel.UpdateProgression(task.Stats);
        ClearModifiers();
    }

    public void UpdateModifiers(int assigneeIndex)
    {
        if(characterCache.ContainsKey(assigneeIndex))
        {
            CharacterController assignee = characterCache[assigneeIndex];
            taskProgressionPanel.UpdateModifiers(task.Stats, assignee.Stats);
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

    public void UpdateActionButton()
    {
        if(addToSprintButton == null || removeFromSprintButton == null) { return; } // TODO: Separate button logic from task detail panel.
        if(task.Status == TaskStatus.BACKLOG)
        {
            addToSprintButton.gameObject.SetActive(true);
            removeFromSprintButton.gameObject.SetActive(false);

            // Button is only interactable when assignee is selected.
            addToSprintButton.interactable = (task.Assignee != null);
        }
        else
        {
            addToSprintButton.gameObject.SetActive(false);
            removeFromSprintButton.gameObject.SetActive(true);
        }
    }

    void AddCharactersToAssigneeOptions()
    {
        characterCache = new Dictionary<int, CharacterController>();
        List<Sprite> portraits = new List<Sprite>();
        int index = 1; // 0 Represents unassigned.
        foreach (CharacterController character in CharacterManager.Instance.Characters)
        {
            characterCache.Add(index++, character);
            portraits.Add(character.Portrait);
        }
        assigneeSelection.AddOptions(portraits);
    }

    public Task Task
    {
        get { return task; }
        set { task = value; }
    }
}
