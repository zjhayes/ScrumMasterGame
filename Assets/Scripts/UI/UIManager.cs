using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    Canvas overheadCanvas;
    [SerializeField]
    SelectedIcon selectedCharacterIcon;
    [SerializeField]
    GameObject frustrationSpeechBubblePrefab;
    [SerializeField]
    CharacterCardController characterCard;
    [SerializeField]
    GameObject taskPanelPrefab;
    [SerializeField]
    PlanningWindow planningWindow;

    public TaskPanel CreateTaskPanel(Task task, Transform parent)
    {
        taskPanelPrefab.GetComponent<TaskPanel>().Task = task;
        TaskPanel taskPanel = Instantiate(taskPanelPrefab, parent).GetComponent<TaskPanel>();
        return taskPanel;
    }

    public void CreateFrustrationSpeechBubble(OverheadController controller)
    {
        GameObject frustrationSpeechBubble = Instantiate(frustrationSpeechBubblePrefab);
        frustrationSpeechBubble.transform.SetParent(overheadCanvas.transform);
        frustrationSpeechBubble.GetComponent<SpeechBubble>().Show(controller);
    }

    public Canvas OverheadCanvas
    {
        get { return overheadCanvas; }
    }

    public SelectedIcon SelectedCharacterIcon
    {
        get { return selectedCharacterIcon; }
    }

    public CharacterCardController CharacterCard
    {
        get { return characterCard; }
    }

    public PlanningWindow PlanningWindow
    {
        get { return planningWindow; }
    }
}
