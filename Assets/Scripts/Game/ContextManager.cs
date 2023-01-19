using UnityEngine;
using UnityEngine.EventSystems;

public class ContextManager : Singleton<ContextManager>, IController
{
    [SerializeField]
    CameraController camera;

    public delegate void OnEnableInteractables();
    public OnEnableInteractables onEnableInteractables;

    public delegate void OnDisableInteractables();
    public OnDisableInteractables onDisableInteractables;

    public delegate void OnStateTransitioned(GameState state);
    public OnStateTransitioned onStateTransitioned;

    StateContext<ContextManager> stateContext;
    CharacterController currentCharacter;

    protected override void Awake()
    {
        base.Awake();
        stateContext = new StateContext<ContextManager>(this);
    }

    void Start()
    {
        Default();
        // Deselect all when player hits Escape, and when sprint ends.
        PlayerControls.Instance.onEscape += EscapeCurrentState;
        SprintManager.Instance.onBeginRetrospective += Default;
        // Set controls for displaying window.
        PlayerControls.Instance.onShowBoard += ToggleBoardView;
    }

    public void Default()
    {
        stateContext.Transition<DefaultState>();
    }

    public void ToggleBoardView()
    {
        if(CurrentState is BoardViewState)
        {
            Default(); // Back out to default view.
        }
        else
        {
            stateContext.Transition<BoardViewState>();
        }
    }

    public void CharacterSelected(CharacterController character)
    {
        currentCharacter = character;
        stateContext.Transition<SelectedCharacterState>();
    }

    public void EscapeCurrentState()
    {
        CurrentState.Escape();
    }

    // TODO: Move interactable logic to own controller
    public void EnableInteractables()
    {
        onEnableInteractables?.Invoke();
    }

    public void DisableInteractables()
    {
        onDisableInteractables?.Invoke();
    }

    public GameState CurrentState
    {
        get { return (GameState)stateContext.CurrentState; }
    }

    public CharacterController CurrentCharacter
    {
        get { return currentCharacter; }
        set { currentCharacter = value; }
    }

    public CameraController Camera
    {
        get { return camera; }
    }
}
