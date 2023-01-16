using System.Collections;
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
    ProgressBar usabilityProgressBar;
    [SerializeField]
    ProgressBar stabilityProgressBar;
    [SerializeField]
    ProgressBar functionalityProgressBar;
    [SerializeField]
    ProgressBar maintainabilityProgressBar;
    [SerializeField]
    TMP_Dropdown assigneeSelection;
    [SerializeField]
    Color positiveColor;
    [SerializeField]
    Color negativeColor;
    [SerializeField]
    TextMeshProUGUI usabilityModifier;
    [SerializeField]
    TextMeshProUGUI stabilityModifier;
    [SerializeField]
    TextMeshProUGUI functionalityModifier;
    [SerializeField]
    TextMeshProUGUI maintainabilityModifier;
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
        task = null;
        base.Hide();
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
        ClearModifiers();
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
        usabilityProgressBar.CurrentFill = task.Stats.Usability;
        stabilityProgressBar.CurrentFill = task.Stats.Stability;
        functionalityProgressBar.CurrentFill = task.Stats.Functionality;
        maintainabilityProgressBar.CurrentFill = task.Stats.Maintainability;
        ClearModifiers();
    }

    public void UpdateModifiers(int assigneeIndex)
    {
        if(characterCache.ContainsKey(assigneeIndex))
        {
            CharacterController assignee = characterCache[assigneeIndex];
            UpdateModifier(usabilityModifier, CalculateModifier(task.Stats.Usability, assignee.Stats.Frontend));
            UpdateModifier(stabilityModifier, CalculateModifier(task.Stats.Stability, assignee.Stats.Backend, assignee.Stats.ProblemSolving));
            UpdateModifier(functionalityModifier, CalculateModifier(task.Stats.Functionality, assignee.Stats.Frontend, assignee.Stats.Backend));
            UpdateModifier(maintainabilityModifier, CalculateModifier(task.Stats.Maintainability, assignee.Stats.ProblemSolving));
        }
        else
        {
            ClearModifiers();
        }
    }

    public void ClearModifiers()
    {
        usabilityModifier.text = "";
        stabilityModifier.text = "";
        functionalityModifier.text = "";
        maintainabilityModifier.text = "";
    }

    void UpdateModifier(TextMeshProUGUI modifierText, int modifier)
    {
        if (modifier >= 0)
        {
            modifierText.text = "+" + modifier.ToString();
            modifierText.color = positiveColor;
        }
        else
        {
            modifierText.text = modifier.ToString();
            modifierText.color = negativeColor;
        }
    }

    int CalculateModifier(int productionStat, int characterStat1, int characterStat2 = 0)
    {
        return (characterStat1 + characterStat2) - productionStat;
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
