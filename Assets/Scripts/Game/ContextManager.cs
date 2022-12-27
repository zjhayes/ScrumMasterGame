using UnityEngine;
using UnityEngine.EventSystems;

public class ContextManager : Singleton<ContextManager>
{
    public delegate void OnCharacterSelected();
    public OnCharacterSelected onCharacterSelected;

    public delegate void OnDeselect();
    public OnDeselect onDeselect;

    CharacterController currentCharacter;

    /*public void OnMenuSelected(MenuItem menuItem, PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }*/

    public void SwitchToCharacterContext(CharacterController character)
    {
        currentCharacter = character;
        onCharacterSelected?.Invoke();
    }

    public void SwitchToNoContext(PointerEventData eventData)
    {
        currentCharacter = null;
        onDeselect?.Invoke();
    }

    public CharacterController CurrentCharacter
    {
        get { return currentCharacter; }
    }
}
