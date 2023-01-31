using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    SelectedIcon selectedCharacterIcon;
    [SerializeField]
    CharacterCard characterCard;
    [SerializeField]
    PlanningMenuController planningMenu;
    [SerializeField]
    ScrumMenuController scrumMenu;

    public delegate void OnShowFrustrationEmote(OverheadController overheadController);
    public event OnShowFrustrationEmote onShowFrustrationEmote;

    public void ShowFrustrationEmote(OverheadController overheadController)
    {
        onShowFrustrationEmote?.Invoke(overheadController);
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
