using UnityEngine;

[RequireComponent(typeof(CharacterInteraction))]
public class CharacterController : MonoBehaviour, IController
{
    [SerializeField]
    float walkingSpeed = 2.25f;
    [SerializeField]
    float runningSpeed = 5f;

    PlayerInput input;
    CharacterInteraction interaction;
    MovementHandler movementHandler;
    bool isRunning = false;
    float moveVerticle = 0.0f;
    float moveHorizontal = 0.0f;

    void Awake()
    {
        input = new PlayerInput();
        interaction = GetComponent<CharacterInteraction>();
    }

    void Start()
    {
        movementHandler = new MovementHandler(transform);

        // Assign input controls to player movement.
        input.Character.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        input.Character.Move.canceled += ctx => Stop();
        input.Character.Run.started += _ => Run();
        input.Character.Run.canceled += _ => Walk();
        input.Character.Interact.performed += _ => Interact();
    }

    void FixedUpdate()
    {
        movementHandler.Move(Direction, Speed);
    }

    void Move(Vector2 direction)
    {
        moveVerticle = direction.y;
        moveHorizontal = direction.x;
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

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input?.Disable();
    }
}