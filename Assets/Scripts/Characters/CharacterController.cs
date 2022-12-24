using UnityEngine;

[RequireComponent(typeof(Selectable))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float walkingSpeed = 2.25f;
    [SerializeField]
    float runningSpeed = 5f;

    CharacterInventory inventory;
    bool isRunning = false;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;

    Selectable selectability;

    void Awake()
    {
        selectability = GetComponent<Selectable>();
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

    public void GoTo(Interactable interactable)
    {
        Debug.Log(this.gameObject.name + " goes to " + interactable.gameObject.name);
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