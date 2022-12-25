using UnityEngine;

/** An Interactable is a Selectable that requires a Character to interact with. **/
public class Interactable : Selectable
{
    [SerializeField]
    Transform goToPosition; // Optional, position character will walk to.

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

    public Vector3 Position
    {
        get
        {
            if(goToPosition)
            {
                return goToPosition.position;
            }
            else
            {
                return transform.position;
            }
        }
    }
}