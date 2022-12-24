using UnityEngine;

[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float walkingSpeed = 2.25f;
    [SerializeField]
    float runningSpeed = 5f;

    Selectable selectability;
    CharacterMovement movement;
    CharacterInventory inventory;

    bool isRunning = false;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;

    void Awake()
    {
        selectability = GetComponent<Selectable>();
        movement = GetComponent<CharacterMovement>();
        inventory = GetComponent<CharacterInventory>();
    }

    void Start()
    {
        selectability.onSelect += OnSelect;
    }

    void OnSelect()
    {
        ContextManager.Instance.SwitchToCharacterContext(this);
    }

    public void GoInteractWith(Interactable interactable)
    {
        movement.GoTo(interactable.transform.position);
    }

    /** OLD STUFF **/

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
    }
}