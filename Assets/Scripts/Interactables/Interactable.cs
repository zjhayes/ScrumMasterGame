using UnityEngine;

/** An Interactable is a Selectable that requires a Character to interact with. **/
public class Interactable : Selectable
{ 
    [SerializeField]
    Transform goToPosition; // Optional, position character will stand to interact.

    public ICharacterController claimedBy;

    public delegate void OnInteract(ICharacterController character);
    public event OnInteract onInteract;

    void Start()
    {
        gameManager.Interactables.onEnableInteractables += Enable;
        gameManager.Interactables.onDisableInteractables += Disable;
        Disable();
    }

    protected override void Select()
    {
        if(gameManager.Context.CurrentCharacter != null) // A character must be selected.
        {
            ICharacterController character = gameManager.Context.CurrentCharacter;
            character.GoInteractWith(this);
            base.Select();
        }
    }

    public virtual void InteractWith(ICharacterController character)
    {
        onInteract?.Invoke(character);
    }

    void OnDestroy()
    {
        gameManager.Interactables.onEnableInteractables -= Enable;
        gameManager.Interactables.onDisableInteractables -= Disable;
    }

    public ICharacterController ClaimedBy { get; set; }

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