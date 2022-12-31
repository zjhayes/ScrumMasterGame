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
    CharacterStatus status;
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
    }

    void OnSelect()
    {
        ContextManager.Instance.CharacterSelected(this);
    }

    public void Idle()
    {
        currentInteractable = null;
        status = CharacterStatus.IDLE;
        stateContext.Transition<IdleState>();
    }

    public void GoInteractWith(Interactable interactable)
    {
        currentInteractable = interactable;
        status = CharacterStatus.MOVING;
        stateContext.Transition<GoToInteractableState>();
        
    }

    public void InteractWithCurrent()
    {
        status = CharacterStatus.WORKING;
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

    public CharacterStatus Status
    {
        get { return status; }
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

    /** OLD STUFF


    [SerializeField]
    float walkingSpeed = 2.25f;
    [SerializeField]
    float runningSpeed = 5f;

    bool isRunning = false;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;
    public void Move(Vector2 direction)
    {
        moveVerticle = direction.y;
        moveHorizontal = direction.x;
    }

    public void Stop()
    {
        moveVerticle = 0f;
        moveHorizontal = 0f;
    }

    public void Run()
    {
        isRunning = true;
    }

    public void Walk()
    {
        isRunning = false;
    }

    public Vector3 Direction
    {
        get
        {
            return new Vector3(-moveHorizontal, Numeric.ZERO, -moveVerticle);
        }
    }

    public float Speed
    {
        get
        {
            return isRunning ? runningSpeed : walkingSpeed;
        }
    }

    public CharacterMovement Movement
    {
        get { return movement; }
    }

    public CharacterInventory Inventory
    {
        get { return inventory; }
    }

    public void EnablePhysics(bool enable)
    {
        GetComponent<Rigidbody>().useGravity = enable;
        GetComponent<Rigidbody>().isKinematic = !enable;
        GetComponent<Collider>().enabled = enable;
    }**/
}

public enum CharacterStatus
{
    IDLE,
    MOVING,
    WORKING
}