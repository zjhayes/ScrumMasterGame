using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMovement))]
public class CharacterController : GameBehaviour, ICharacterController, ISocketable
{
    [SerializeField]
    private Sprite portrait;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private CharacterState idleState;
    [SerializeField]
    private CharacterState goToInteractableState;
    [SerializeField]
    private CharacterState interactionState;
    [SerializeField]
    private CharacterState findSomethingToDoState;
    [SerializeField]
    private CharacterState frustratedState;
    [SerializeField]
    private Selectable selectability;
    [SerializeField]
    private OverheadElement selectIcon;

    private CharacterStats stats;
    private CharacterMovement movement;
    private CharacterContext context;
    private Interactable targetInteractable;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        movement = GetComponent<CharacterMovement>();
        context = new CharacterContext(this, gameManager);
    }

    private void Start()
    {
        selectability.OnSelect += OnSelect;
        Idle();
    }

    private void OnSelect()
    {
        // Context Manager determines how to handle character selection.
        gameManager.Context.CharacterSelected(this);
        selectIcon.Show();
    }

    public void Deselect()
    {
        selectIcon.Hide();
    }

    public void Idle()
    {
        context.TransitionTo(CharacterStates.IDLE);
    }

    public void FindSomethingToDo()
    {
        ClearTargetInteractable();
        context.TransitionTo(CharacterStates.FIND_SOMETHING_TO_DO);
    }

    // Character moves to interactable to interact.
    public void GoInteractWith(Interactable interactable)
    {
        targetInteractable = interactable;
        context.TransitionTo(CharacterStates.GO_TO_INTERACTABLE);
    }

    public void InteractWithTarget()
    {
        context.TransitionTo(CharacterStates.INTERACT);
    }

    public void Frustrated()
    {
        ClearTargetInteractable();
        context.TransitionTo(CharacterStates.FRUSTRATED);
    }

    public void ClearTargetInteractable()
    {
        targetInteractable = null;
    }

    public CharacterStats Stats
    {
        get { return stats; }
    }

    public CharacterMovement Movement
    {
        get { return movement; }
    }

    public Inventory Inventory
    {
        get { return inventory; }
    }

    public Interactable TargetInteractable
    {
        get { return targetInteractable; }
    }

    public CharacterContext Context
    {
        get { return context; }
    }

    public CharacterState State
    {
        get { return context.CurrentState; }
    }

    public Sprite Portrait
    {
        get { return portrait; }
    }

    public void EnablePhysics(bool enable)
    {
        //GetComponent<Collider>().enabled = enable; // TODO: Determine what to do with character collider
        GetComponent<NavMeshAgent>().enabled = enable;
    }
}