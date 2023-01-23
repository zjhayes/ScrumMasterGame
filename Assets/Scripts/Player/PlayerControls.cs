using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : Singleton<PlayerControls>
{
    PlayerInput input;

    public delegate void OnEscape();
    public OnEscape onEscape;

    public delegate void OnShowBoard();
    public OnShowBoard onShowBoard;

    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInput();
    }

    void Start()
    {
        input.Player.Escape.canceled += _ => Escape();
        input.Player.ShowBoard.canceled += _ => ShowBoard();
    }

    void ShowBoard()
    {
        onShowBoard?.Invoke();
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
