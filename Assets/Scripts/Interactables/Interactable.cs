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
        gameManager.Context.onEnableInteractables += Enable;
        gameManager.Context.onDisableInteractables += Disable;
        Disable();
    }

    protected override void Select()
    {
        if(gameManager.Context.CurrentCharacter) // A character must be selected.
        {
            CharacterController character = gameManager.Context.CurrentCharacter;
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