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
        // Listen to Sprint Manager.
        GameManager.Instance.Sprint.onBeginPlanning += SwitchToPlanningView;
        GameManager.Instance.Sprint.onBeginSprint += Default;

        // Listen to player controls.
        GameManager.Instance.Controls.onEscape += EscapeCurrentState;
        GameManager.Instance.Controls.onShowBoard += ToggleScrumBoard;
    }

    public void ToggleScrumBoard()
    {
        if (CurrentState is ScrumViewState)
        {
            Default();
        }
        else if (CurrentState is DefaultState || CurrentState is SelectedCharacterState)
        {
            SwitchToScrumView();
        } // Else do nothing, invalid context.
    }

    public void Default()
    {
        stateContext.Transition<DefaultState>();
    }

    public void SwitchToScrumView()
    {
        stateContext.Transition<ScrumViewState>();
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
        Application.Quit();
#endif
    }

    void OnDisable()
    {
        // Stop listening to Sprint Manager.
        GameManager.Instance.Sprint.onBeginPlanning -= SwitchToPlanningView;
        GameManager.Instance.Sprint.onBeginSprint -= Default;
        GameManager.Instance.Controls.onEscape -= EscapeCurrentState;
        GameManager.Instance.Controls.onShowBoard -= ToggleScrumBoard;
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
