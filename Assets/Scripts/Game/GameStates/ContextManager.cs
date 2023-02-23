using UnityEngine;
using UnityEditor;

public class ContextManager : GameBehaviour, IContextManager
{
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
        gameManager.Controls.onChangeView += ChangeView;
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

    // TODO: State controls can be moved to own class.
    public void ChangeView()
    {
        CurrentState.ChangeView();
    }

    public void EscapeCurrentState()
    {
        CurrentState.Escape();
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
        gameManager.Controls.onChangeView -= ChangeView;
    }

    public GameState CurrentState
    {
        get { return stateContext.CurrentState as GameState; }
    }

    public ICharacterController CurrentCharacter
    {
        get { return currentCharacter; }
        set { currentCharacter = value; }
    }
}
