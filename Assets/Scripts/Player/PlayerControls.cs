using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : Singleton<PlayerControls>
{
    PlayerInput input;

    public delegate void OnEscape();
    public OnEscape onEscape;

    void Awake()
    {
        base.Awake();
        input = new PlayerInput();
    }

    void Start()
    {
        // Assign controls to character movement.
        //input.Character.Move.performed += ctx => character.Move(ctx.ReadValue<Vector2>());
        // input.Character.Move.canceled += ctx => character.Stop();
        // input.Character.Run.started += _ => character.Run();
        // input.Character.Run.canceled += _ => character.Walk();
        input.Player.Escape.canceled += _ => Escape();

        
    }

    void Escape()
    {
        onEscape?.Invoke();
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
