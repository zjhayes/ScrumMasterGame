using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    OverheadCanvasController overheadCanvas;
    [SerializeField]
    CharacterCard characterCard;
    [SerializeField]
    PlanningMenuController planningMenu;
    [SerializeField]
    ScrumMenuController scrumMenu;
    [SerializeField]
    StatusBarController statusBar;

    public OverheadCanvasController OverheadCanvas
    {
        get { return overheadCanvas; }
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

    public StatusBarController StatusBar
    {
        get { return statusBar; }
    }
}
