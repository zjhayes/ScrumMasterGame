using UnityEngine;

/** An Interactable is a Selectable that requires another active Selectable to interact with. **/
public class Interactable : Selectable
{
    public delegate void OnInteract(SelectableCharacter character);
    public OnInteract onInteract;

    void Start()
    {
        ContextManager.Instance.onCharacterSelected += Enable;
        ContextManager.Instance.onDeselect += Disable;
        Disable();
    }

    protected override void Select()
    {
        if(ContextManager.Instance.CurrentSelection)
        {
            SelectableCharacter character = ContextManager.Instance.CurrentSelection;
            InteractWith(character);
            base.Select();
        }
    }

    public void InteractWith(SelectableCharacter character)
    {
        Debug.Log(character.gameObject.name + " interacts with " + this.gameObject.name);
        onInteract?.Invoke(character);
    }
}