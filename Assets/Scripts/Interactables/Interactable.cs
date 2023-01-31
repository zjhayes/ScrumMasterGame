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
        GameManager.Instance.Context.onEnableInteractables += Enable;
        GameManager.Instance.Context.onDisableInteractables += Disable;
        Disable();
    }

    protected override void Select()
    {
        if(GameManager.Instance.Context.CurrentCharacter) // A character must be selected.
        {
            CharacterController character = GameManager.Instance.Context.CurrentCharacter;
            character.GoInteractWith(this);
            base.Select();
        }
    }

    public virtual void InteractWith(CharacterController character)
    {
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