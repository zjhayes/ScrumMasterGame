using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    CharacterCard characterCard;
    [SerializeField]
    PlanningMenuController planningMenu;
    [SerializeField]
    ScrumMenuController scrumMenu;
    [SerializeField]
    RetrospectiveMenuController retrospectiveMenu;
    [SerializeField]
    StatusBarController statusBar;

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

    public RetrospectiveMenuController RetrospectiveMenu
    {
        get { return retrospectiveMenu; }
    }

    public StatusBarController StatusBar
    {
        get { return statusBar; }
    }
}
