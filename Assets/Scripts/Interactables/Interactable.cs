using UnityEngine;

/** An Interactable is a Selectable that requires a Character to interact with. **/
public abstract class Interactable : Selectable
{ 
    [SerializeField]
    Transform goToPosition; // Optional, position character will stand to interact.

    public ICharacterController claimedBy;

    public delegate void OnInteract(ICharacterController character);
    public event OnInteract onInteract;

    void Start()
    {
<<<<<<< Updated upstream
        gameManager.Interactables.AddOpenInteractable(this);
        gameManager.Interactables.onEnableInteractables += Enable;
        gameManager.Interactables.onDisableInteractables += Disable;
        Disable();
=======
        gameManager.Interactables.onEnableInteractables += EnableSelection;
        gameManager.Interactables.onDisableInteractables += DisableSelection;
        DisableSelection();
>>>>>>> Stashed changes
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

    // Returns score based on how likely this character needs this interaction.
    public abstract int CalculatePriorityFor(ICharacterController character);

    void OnEnable()
    {
        // Make self available to characters.
        gameManager.Interactables.AddOpenInteractable(this);
    }

    void OnDisable()
    {
        // Make self unavailable for use.
        gameManager.Interactables.RemoveOpenInteractable(this);
    }

    void OnDestroy()
    {
        gameManager.Interactables.onEnableInteractables -= EnableSelection;
        gameManager.Interactables.onDisableInteractables -= DisableSelection;
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