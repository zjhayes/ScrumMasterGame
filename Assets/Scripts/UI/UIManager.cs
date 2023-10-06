using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CharacterCard characterCard;
    [SerializeField]
    private PlanningMenuController planningMenu;
    [SerializeField]
    private ScrumMenuController scrumMenu;
    [SerializeField]
    private RetrospectiveMenuController retrospectiveMenu;
    [SerializeField]
    private StatusBarController statusBar;
    [SerializeField]
    private IconManager icons;

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

    public IconManager Icons
    {
        get { return icons; }
    }
}
