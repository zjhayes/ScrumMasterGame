using UnityEngine;
using UnityEditor;

public class ContextManager : GameBehaviour, IContextManager
{
    public event OnEnableInteractables onEnableInteractables;
    public event OnDisableInteractables onDisableInteractables;

    StateContext<ContextManager> stateContext;
    ICharacterController currentCharacter;

    [SerializeField]
    GameState defaultState;
    [SerializeField]
    GameState scrumViewState;
    [SerializeField]
    GameState planningViewState;
    [SerializeField]
    GameState selectedCharacterState;

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
        stateContext.Transition<GameState>(defaultState);
    }

    public void SwitchToScrumView()
    {
        stateContext.Transition<GameState>(scrumViewState);
    }

    public void SwitchToPlanningView()
    {
        stateContext.Transition<GameState>(planningViewState);
    }

    public void CharacterSelected(ICharacterController character)
    {
        currentCharacter = character;
        stateContext.Transition<GameState>(selectedCharacterState);
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

    public IGameState CurrentState
    {
        get { return (IGameState)stateContext.CurrentState; }
    }

    public ICharacterController CurrentCharacter
    {
        get { return currentCharacter; }
        set { currentCharacter = value; }
    }
}
