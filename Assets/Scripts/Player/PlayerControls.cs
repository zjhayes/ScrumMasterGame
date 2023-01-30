using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    PlayerInput input;

    public delegate void OnEscape();
    public OnEscape onEscape;

    public delegate void OnShowBoard();
    public OnShowBoard onShowBoard;

    void Awake()
    {
        input = new PlayerInput();

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
