using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerControls : MonoBehaviour
{
    PlayerInput input;
    CharacterController character;
    InteractionController interaction;


    void Awake()
    {
        input = new PlayerInput();
        character = GetComponent<CharacterController>();
        interaction = GetComponent<InteractionController>();
    }

    void Start()
    { 
        // Assign controls to character movement.
        //input.Character.Move.performed += ctx => character.Move(ctx.ReadValue<Vector2>());
       // input.Character.Move.canceled += ctx => character.Stop();
       // input.Character.Run.started += _ => character.Run();
       // input.Character.Run.canceled += _ => character.Walk();
        //input.Mouse.LeftClick.canceled += _ => OnLeftMouseClick();

        // Assign controls to character interactions.
        input.Character.Interact.started += _ => interaction.Interact();
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input?.Disable();
    }

    public PlayerInput Input
    {
        get { return input; }
    }
}
