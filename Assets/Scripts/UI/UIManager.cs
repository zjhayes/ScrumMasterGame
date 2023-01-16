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
    PlanningWindow planningWindow;

    public void CreateFrustrationSpeechBubble(OverheadController controller)
    {
        GameObject frustrationSpeechBubble = Instantiate(frustrationSpeechBubblePrefab);
        frustrationSpeechBubble.transform.SetParent(overheadCanvas.transform);
        frustrationSpeechBubble.GetComponent<SpeechBubble>().Show(controller);
    }
    /*
    public Canvas OverheadCanvas
    {
        get { return overheadCanvas; }
    }*/

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
