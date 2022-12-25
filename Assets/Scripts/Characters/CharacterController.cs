using UnityEngine;

[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(Inventory))]
public class CharacterController : MonoBehaviour, IController
{
    Selectable selectability;
    CharacterMovement movement;
    Inventory inventory;
    StateContext<CharacterController> stateContext;

    Interactable currentInteractable;

    void Awake()
    {
        selectability = GetComponent<Selectable>();
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<Inventory>();
        stateContext = new StateContext<CharacterController>(this);
    }

    void Start()
    {
        selectability.onSelect += OnSelect;
    }

    void OnSelect()
    {
        ContextManager.Instance.SwitchToCharacterContext(this);
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

    public void EnablePhysics(bool enable)
    {
        GetComponent<Rigidbody>().useGravity = enable;
        GetComponent<Rigidbody>().isKinematic = !enable;
        GetComponent<Collider>().enabled = enable;
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