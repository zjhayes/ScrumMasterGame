using UnityEngine;

/* An object that a character can be directed to interact with. */
public abstract class Interactable : GameBehaviour
{
    [SerializeField]
    private Selectable selectability;
    [SerializeField]
    private Transform goToPosition; // Optional, position character will stand to interact.

    public delegate void OnInteract(ICharacterController character);
    public event OnInteract onInteract;

    private void Start()
    {
        // Listen to global changes to interactables.
        gameManager.Interactables.onEnableInteractables += selectability.EnableSelection;
        gameManager.Interactables.onDisableInteractables += selectability.DisableSelection;

        // Listen to selection.
        selectability.onSelect += OnSelect;
        selectability.DisableSelection();
    }

    private void OnSelect()
    {
        if(gameManager.Context.CurrentCharacter != null) // A character must be selected.
        {
            ICharacterController character = gameManager.Context.CurrentCharacter;
            character.GoInteractWith(this);
        }
    }

    public virtual void InteractWith(ICharacterController character)
    {
        onInteract?.Invoke(character);
    }

    // Returns score based on how likely this character needs this interaction.
    public abstract int CalculatePriorityFor(ICharacterController character);

    // Override with conditions for a character interacting with this.
    public abstract bool CanInteract(ICharacterController character);

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

    private void OnEnable()
    {
        // Make self available to characters.
        gameManager.Interactables.AddOpenInteractable(this);
    }

    private void OnDisable()
    {
        // Make self unavailable for use.
        gameManager.Interactables.RemoveOpenInteractable(this);
    }

    private void OnDestroy()
    {
        gameManager.Interactables.onEnableInteractables -= selectability.EnableSelection;
        gameManager.Interactables.onDisableInteractables -= selectability.DisableSelection;
    }
}