using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskDetailsPanel : MonoBehaviour
{
    [SerializeField]
    Image taskTypeIcon;
    [SerializeField]
    TextMeshProUGUI summaryText;
    [SerializeField]
    TextMeshProUGUI descriptionText;
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

    Task task;
    Dictionary<int, CharacterController> characterCache;

    void Start()
    {
        assigneeSelection.onValueChanged.AddListener(delegate { OnAssigneeSelected(); });

        // Set up assignee selection.
        if (characterCache == null || characterCache.Count <= 0)
        {
            AddCharactersToAssigneeOptions();
        }

        UpdateDetails();
        UpdateAssignee();
    }

    public void Show()
    {
        if(task != null)
        {
            gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No task set on Task Details panel.");
        }
    }

    public void Hide()
    {
        task = null;
        gameObject.SetActive(false);
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
    }

    public void UpdateAssignee()
    {
        // Find task assignee's option index in character cache.
        int assigneeKey = characterCache.FirstOrDefault(x => x.Value == task.Assignee).Key;
        assigneeSelection.value = assigneeKey;
    }

    public void UpdateDetails()
    {
        taskTypeIcon.sprite = task.TaskTypeIcon;
        summaryText.text = task.Summary;
        descriptionText.text = task.Description;
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
            Debug.Log(modifierText.text);
        }
        else
        {
            modifierText.text = modifier.ToString();
            modifierText.color = negativeColor;
        }
    }

    int CalculateModifier(int productionStat, int characterStat1, int characterStat2 = 0)
    {
        return productionStat - (characterStat1 + characterStat2);
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

    public bool IsShowing
    {
        get { return gameObject.active; }
    }
}
