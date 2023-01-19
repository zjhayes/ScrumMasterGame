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
    CharacterCard characterCard;
    [SerializeField]
    PlanningWindow planningWindow;
    [SerializeField]
    ScrumMenuController scrumMenu;

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

    public CharacterCard CharacterCard
    {
        get { return characterCard; }
    }

    public PlanningWindow PlanningWindow
    {
        get { return planningWindow; }
    }

    public ScrumMenuController ScrumMenu
    {
        get { return scrumMenu; }
    }
}
