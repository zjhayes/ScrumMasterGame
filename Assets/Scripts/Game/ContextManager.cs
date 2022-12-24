using UnityEngine;
using UnityEngine.EventSystems;

public class ContextManager : Singleton<ContextManager>
{
    public delegate void OnCharacterSelected();
    public OnCharacterSelected onCharacterSelected;

    public delegate void OnDeselect();
    public OnDeselect onDeselect;

    SelectableCharacter currentSelection;

    /*public void OnMenuSelected(MenuItem menuItem, PointerEventData eventData)
    {
        Debug.Log(eventData.position);
    }*/

    public void SwitchToCharacterContext(SelectableCharacter character)
    {
        currentSelection = character;
        onCharacterSelected?.Invoke();
        Debug.Log("Switching Contexts: Character");
    }

    public void OnInteractableSelected(Interactable target)
    {
        if(currentSelection)
        {
            target.InteractWith(currentSelection);
        }
        Debug.Log("Selected " + target.gameObject.name);
    }

    public void SwitchToNoContext(PointerEventData eventData)
    {
        currentSelection = null;
        onDeselect?.Invoke();
    }

    public SelectableCharacter CurrentSelection
    {
        get { return currentSelection; }
    }
}
