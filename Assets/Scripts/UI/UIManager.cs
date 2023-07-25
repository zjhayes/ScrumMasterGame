using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    OverheadCanvasController overheadCanvas;
    [SerializeField]
    SelectedIcon selectedCharacterIcon;
    [SerializeField]
    CharacterCard characterCard;
    [SerializeField]
    PlanningMenuController planningMenu;
    [SerializeField]
    ScrumMenuController scrumMenu;

    public OverheadCanvasController OverheadCanvas
    {
        get { return overheadCanvas; }
    }
    
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
