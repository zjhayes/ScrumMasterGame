using UnityEngine;

public class UIManager : MonoBehaviour
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
    PlanningMenuController planningMenu;
    [SerializeField]
    ScrumMenuController scrumMenu;

    void Start()
    {
        GameManager.Instance.UI = this;
    }

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

    public PlanningMenuController PlanningMenu
    {
        get { return planningMenu; }
    }

    public ScrumMenuController ScrumMenu
    {
        get { return scrumMenu; }
    }
}
