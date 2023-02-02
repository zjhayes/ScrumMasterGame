using UnityEngine;
using UnityEditor;

public class ContextManager : GameBehaviour, IController
{
    public delegate void OnEnableInteractables();
    public OnEnableInteractables onEnableInteractables;

    public delegate void OnDisableInteractables();
    public OnDisableInteractables onDisableInteractables;

    public delegate void OnStateTransitioned(GameState state);
    public OnStateTransitioned onStateTransitioned;

    StateContext<ContextManager> stateContext;
    CharacterController currentCharacter;

    void Awake()
    {
        stateContext = new StateContext<ContextManager>(this);
        Default();

        // Listen to Sprint Manager.
        gameManager.Sprint.onBeginPlanning += SwitchToPlanningView;
        gameManager.Sprint.onBeginSprint += Default;

        // Listen to player controls.
        gameManager.Controls.onEscape += EscapeCurrentState;
        gameManager.Controls.onShowBoard += ToggleScrumBoard;
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
        gameManager.Sprint.onBeginPlanning -= SwitchToPlanningView;
        gameManager.Sprint.onBeginSprint -= Default;
        gameManager.Controls.onEscape -= EscapeCurrentState;
        gameManager.Controls.onShowBoard -= ToggleScrumBoard;
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
}
