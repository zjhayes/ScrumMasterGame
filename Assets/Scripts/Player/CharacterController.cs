using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(CharacterInteraction))]
public class CharacterController : MonoBehaviour
{
    [SerializeField]
    float walkingSpeed = 2.25f;
    [SerializeField]
    float runningSpeed = 5f;

    PlayerInput input;
    UnityEngine.AI.NavMeshAgent agent;
    CharacterInteraction interaction;
    bool isRunning = false;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;

    void Awake()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        input = new PlayerInput();
        interaction = GetComponent<CharacterInteraction>();
    }

    void Start()
    {
        // Assign input controls to player movement.
        input.Character.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        input.Character.Move.canceled += _ => Stop();
        input.Character.Run.started += _ => Run();
        input.Character.Run.canceled += _ => Walk();
        input.Character.Interact.performed += _ => Interact();
    }

    void LateUpdate()
    {
        Vector3 movement = new Vector3(-moveHorizontal, 0f, -moveVerticle);
        Vector3 moveDestination = transform.position + movement;
        agent.speed = this.Speed;
        agent.destination = moveDestination;
    }

    void Move(Vector2 direction)
    {
        moveVerticle = direction.y;
        moveHorizontal = direction.x;
    }

    void Stop()
    {
        moveVerticle = 0f;
        moveHorizontal = 0f;
    }

    void Run()
    {
        isRunning = true;
    }

    void Walk()
    {
        isRunning = false;
    }

    void Interact()
    {
        interaction.Interact();
    }

    public float Speed
    {
        get
        {
            return isRunning ? runningSpeed : walkingSpeed;
        }
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
}