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

    Task task;
    Dictionary<int, CharacterController> characterCache;

    void Start()
    {
        assigneeSelection.onValueChanged.AddListener(delegate { OnAssigneeSelected(); });

        characterCache = new Dictionary<int, CharacterController>();
        List<Sprite> portraits = new List<Sprite>();
        int index = 1; // 0 Represents unassigned.
        foreach(CharacterController character in CharacterManager.Instance.Characters)
        {
            characterCache.Add(index++, character);
            portraits.Add(character.Portrait);
        }
        assigneeSelection.AddOptions(portraits);
    }

    public void Show()
    {
        UpdateDetails(task);
        UpdateAssignee();
        gameObject.SetActive(true);
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
    }

    public void UpdateAssignee()
    {
        // Get index of task assignee option.
        int assigneeKey = characterCache.FirstOrDefault(x => x.Value == task.Assignee).Key;
        assigneeSelection.value = assigneeKey;
    }

    public void UpdateDetails(Task task)
    {
        taskTypeIcon.sprite = task.TaskTypeIcon;
        summaryText.text = task.Summary;
        descriptionText.text = task.Description;
        usabilityProgressBar.CurrentFill = task.Stats.Usability;
        stabilityProgressBar.CurrentFill = task.Stats.Stability;
        functionalityProgressBar.CurrentFill = task.Stats.Functionality;
        maintainabilityProgressBar.CurrentFill = task.Stats.Maintainability;
    }

    public Task Task
    {
        get { return task; }
        set { task = value; }
    }
}
