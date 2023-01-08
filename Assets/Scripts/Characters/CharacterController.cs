using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(Inventory))]
public class CharacterController : MonoBehaviour, IController
{
    [SerializeField]
    Sprite portrait;

    CharacterStats stats;
    CharacterMovement movement;
    Selectable selectability;
    Inventory inventory;
    StateContext<CharacterController> stateContext;

    Interactable currentInteractable;

    void Awake()
    {
        stats = GetComponent<CharacterStats>();
        movement = GetComponent<CharacterMovement>();
        selectability = GetComponent<Selectable>();
        inventory = GetComponent<Inventory>();
        stateContext = new StateContext<CharacterController>(this);
    }

    void Start()
    {
        selectability.onSelect += OnSelect;
        Idle();
    }

    void OnSelect()
    {
        ContextManager.Instance.CharacterSelected(this);
    }

    public void Idle()
    {
        currentInteractable = null;
        stateContext.Transition<IdleState>();
    }

    public void GoInteractWith(Interactable interactable)
    {
        currentInteractable = interactable;
        stateContext.Transition<GoToInteractableState>();
        
    }

    public void InteractWithCurrent()
    {
        stateContext.Transition<InteractionState>();
    }

    public void Frustrated()
    {
        GetComponent<OverheadController>()?.ShowFrustrationBubble();
        Idle();
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

    public Interactable CurrentInteractable
    {
        get { return currentInteractable; }
    }

    public StateContext<CharacterController> StateContext
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

    public void EnablePhysics(bool enable)
    {
        GetComponent<Rigidbody>().useGravity = enable;
        GetComponent<Rigidbody>().isKinematic = !enable;
        //GetComponent<Collider>().enabled = enable;
        GetComponent<NavMeshAgent>().enabled = enable;
    }
}