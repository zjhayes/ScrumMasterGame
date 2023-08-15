using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMovement))]
public class CharacterController : GameBehaviour, ICharacterController
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
    private OverheadController overheadController;
    [SerializeField]
    private Selectable selectability;

    private CharacterStats stats;
    private CharacterMovement movement;
    private StateContext<ICharacterController> stateContext;
    private Interactable targetInteractable;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        movement = GetComponent<CharacterMovement>();
        stateContext = new StateContext<ICharacterController>(this);
    }

    private void Start()
    {
        selectability.onSelect += OnSelect;
        Idle();
    }

    private void OnSelect()
    {
        // Context Manager determines how to handle character selection.
        gameManager.Context.CharacterSelected(this);
    }

    public void Idle()
    {
        ClearTargetInteractable();
        stateContext.Transition(idleState);
    }

    public void FindSomethingToDo()
    {
        ClearTargetInteractable();
        stateContext.Transition(findSomethingToDoState);
    }

    // Character moves to interactable to interact.
    public void GoInteractWith(Interactable interactable)
    {
        targetInteractable = interactable;
        stateContext.Transition(goToInteractableState);
    }

    public void InteractWithTarget()
    {
        stateContext.Transition(interactionState);
    }

    public void Frustrated()
    {
        ClearTargetInteractable();
        stateContext.Transition(frustratedState);
    }

    public void ClearTargetInteractable()
    {
        if (targetInteractable != null)
        {
            targetInteractable.ClaimedBy = null;
        }

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

    public StateContext<ICharacterController> StateContext
    {
        get { return stateContext; }
    }

    public CharacterState State
    {
        get { return stateContext.CurrentState as CharacterState; }
    }

    public Sprite Portrait
    {
        get { return portrait; }
    }

    public OverheadController OverHead
    {
        get { return overheadController; }
    }

    public void EnablePhysics(bool enable)
    {
        //GetComponent<Collider>().enabled = enable; // TODO: Determine what to do with character collider
        GetComponent<NavMeshAgent>().enabled = enable;
    }
}