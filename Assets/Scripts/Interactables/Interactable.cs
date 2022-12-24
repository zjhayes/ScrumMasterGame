using UnityEngine;

/** An Interactable is a Selectable that requires a Character to interact with. **/
public class Interactable : Selectable
{
    public delegate void OnInteract(CharacterController character);
    public OnInteract onInteract;

    void Start()
    {
        ContextManager.Instance.onCharacterSelected += Enable;
        ContextManager.Instance.onDeselect += Disable;
        Disable();
    }

    protected override void Select()
    {
        if(ContextManager.Instance.CurrentCharacter)
        {
            CharacterController character = ContextManager.Instance.CurrentCharacter;
            character.GoInteractWith(this);
            base.Select();
        }
    }

    private void InteractWith(CharacterController character)
    {
        Debug.Log(character.gameObject.name + " interacts with " + this.gameObject.name);
        onInteract?.Invoke(character);
    }
}