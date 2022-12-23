using UnityEngine;
using UnityEngine.EventSystems;

public class ContextManager : Singleton<ContextManager>
{
    public delegate void OnCharacterSelected();
    public OnCharacterSelected onCharacterSelected;

    public delegate void OnDeselect();
    public OnDeselect onDeselect;

    /*public void OnMenuSelected(MenuItem menuItem, PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }*/

    public void SwitchToCharacterContext(SelectableCharacter character, PointerEventData eventData)
    {
        onCharacterSelected?.Invoke();
        Debug.Log("Switching Contexts: Character");
    }

    public void OnInteractableSelected(Interactable target, PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }

    public void SwitchToNoContext(PointerEventData eventData)
    {
        onDeselect?.Invoke();
    }
}
