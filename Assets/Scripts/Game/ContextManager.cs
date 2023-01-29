using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class ContextManager : Singleton<ContextManager>, IController
{
    [SerializeField]
    CameraController cameraController;

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

        Default();
    }

    void Start()
    {
        // Listen to player controls.
        PlayerControls.Instance.onEscape += EscapeCurrentState;
    }

    public void Default()
    {
        stateContext.Transition<DefaultState>();
    }

    public void ShowScrumBoard()
    {
        stateContext.Transition<BoardViewState>();
    }

    public void SwitchToPlanningView()
    {
        stateContext.Transition<PlanningViewState>();
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

    // Quit game.
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
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
        get { return cameraController; }
    }
}
