using UnityEngine;
using UnityEditor;

/* Controls the current game state. */
public class ContextManager : GameBehaviour, IContextManager
{
    private StateContext<ContextManager> stateContext;
    private ICharacterController currentCharacter;

    [SerializeField]
    private GameState defaultState;
    [SerializeField]
    private GameState scrumViewState;
    [SerializeField]
    private GameState planningViewState;
    [SerializeField]
    private GameState selectedCharacterState;

    private void Awake()
    {
        stateContext = new StateContext<ContextManager>(this);
        Default();

        // Listen to Sprint Manager.
        gameManager.Sprint.OnBeginPlanning += SwitchToPlanningView;
        gameManager.Sprint.OnBeginSprint += Default;

        // Listen to player controls.
        gameManager.Controls.OnEscape += EscapeCurrentState;
        gameManager.Controls.OnChangeView += ChangeView;
    }

    public void Default()
    {
        stateContext.Transition(defaultState);
    }

    public void SwitchToScrumView()
    {
        stateContext.Transition(scrumViewState);
    }

    public void SwitchToPlanningView()
    {
        stateContext.Transition(planningViewState);
    }

    public void CharacterSelected(ICharacterController character)
    {
        DeselectCharacter();
        currentCharacter = character;
        gameManager.Interactables.EnableInteractables();
        stateContext.Transition(selectedCharacterState);
    }

    public void DeselectCharacter()
    {
        if(currentCharacter != null)
        {
            currentCharacter.Deselect();
            currentCharacter = null;
            gameManager.Interactables.DisableInteractables();
            gameManager.UI.CharacterCard.Hide();
        }
    }
    
    public void ChangeView()
    {
        CurrentState.ChangeView();
    }

    public void EscapeCurrentState()
    {
        CurrentState.OnEscaped();
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

    // Quit game.
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void OnDisable()
    {
        // Stop listening to Sprint Manager.
        gameManager.Sprint.OnBeginPlanning -= SwitchToPlanningView;
        gameManager.Sprint.OnBeginSprint -= Default;
        gameManager.Controls.OnEscape -= EscapeCurrentState;
        gameManager.Controls.OnChangeView -= ChangeView;
    }
}