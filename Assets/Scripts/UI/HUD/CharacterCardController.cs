using UnityEngine;
using UnityEngine.UI;

public class CharacterCardController : MonoBehaviour
{
    [SerializeField]
    Image portraitPanel;

    public void Show(CharacterController character)
    {
        portraitPanel.sprite = character.Portrait;
        gameObject.active = true;
    }

    public void Hide()
    {
        gameObject.active = false;
    }

}
