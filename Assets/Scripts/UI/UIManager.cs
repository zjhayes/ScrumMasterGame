using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    
    [SerializeField]
    SelectedIcon selectedCharacterIcon;

    public SelectedIcon SelectedCharacterIcon
    {
        get { return selectedCharacterIcon; }
    }
}
