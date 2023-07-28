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
<<<<<<< HEAD
        gameManager.Interactables.onEnableInteractables += Enable;
        gameManager.Interactables.onDisableInteractables += Disable;
        Disable();
=======
        gameManager.Interactables.onEnableInteractables += EnableSelection;
        gameManager.Interactables.onDisableInteractables += DisableSelection;
        DisableSelection();
>>>>>>> Stashed changes
=======
        gameManager.Interactables.onEnableInteractables += EnableSelection;
        gameManager.Interactables.onDisableInteractables += DisableSelection;
        DisableSelection();
>>>>>>> c50b138ca4d4a49ae72805c6c44a3b932009d2a3
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
<<<<<<< HEAD
=======
        gameManager.Interactables.RemoveOpenInteractable(this);
>>>>>>> c50b138ca4d4a49ae72805c6c44a3b932009d2a3
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