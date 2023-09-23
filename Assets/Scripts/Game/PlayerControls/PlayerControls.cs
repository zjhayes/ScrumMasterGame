using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControls : MonoBehaviour
{
    PlayerInput input;

    public event Events.PlayerEvent OnEscape;
    public event Events.PlayerEvent OnChangeView;

    void Awake()
    {
        input = new PlayerInput();

        input.Player.Escape.canceled += _ => Escape();
        input.Player.ChangeView.canceled += _ => ChangeView();
    }

    void Start()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input?.Disable();
    }
    void ChangeView()
    {
        OnChangeView?.Invoke();
    }

    void Escape()
    {
        OnEscape?.Invoke();
    }

    public PlayerInput Input
    {
        get { return input; }
    }
}
