using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(Inventory))]
public class CharacterController : GameBehaviour, ICharacterController
{
    [SerializeField]
    Sprite portrait;
    [SerializeField]
    CharacterState idleState;
    [SerializeField]
    CharacterState goToInteractableState;
    [SerializeField]
    CharacterState interactionState;
    [SerializeField]
    CharacterState findSomethingToDoState;
    [SerializeField]
    CharacterState frustratedState;
    [SerializeField]
    OverheadController overheadController;

    CharacterStats stats;
    CharacterMovement movement;
    Selectable selectability;
    Inventory inventory;
    StateContext<ICharacterController> stateContext;

    Interactable targetInteractable;

    void Awake()
    {
        stats = GetComponent<CharacterStats>();
        movement = GetComponent<CharacterMovement>();
        selectability = GetComponent<Selectable>();
        inventory = GetComponent<Inventory>();
        stateContext = new StateContext<ICharacterController>(this);
    }

    void Start()
    {
        selectability.onSelect += OnSelect;
        Idle();
    }

    void OnSelect()
    {
        // Context Manager determines how to handle character selection.
        gameManager.Context.CharacterSelected(this);
    }

    public void Idle()
    {
        ClearTargetInteractable();
        stateContext.Transition<CharacterState>(idleState);
    }

    public void FindSomethingToDo()
    {
        ClearTargetInteractable();
        stateContext.Transition<CharacterState>(findSomethingToDoState);
    }

    // Character moves to interactable to interact.
    public void GoInteractWith(Interactable interactable)
    {
        targetInteractable = interactable;
        stateContext.Transition<CharacterState>(goToInteractableState);
    }

    public void InteractWithTarget()
    {
        stateContext.Transition<CharacterState>(interactionState);
    }

    public void Frustrated()
    {
        ClearTargetInteractable();
        stateContext.Transition<CharacterState>(frustratedState);
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
        //GetComponent<Rigidbody>().useGravity = enable;
        //GetComponent<Rigidbody>().isKinematic = !enable;
        //GetComponent<Collider>().enabled = enable;
        GetComponent<NavMeshAgent>().enabled = enable;
    }
}