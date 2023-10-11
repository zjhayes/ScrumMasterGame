using UnityEngine;
using UnityEditor;

/* Controls the current game state. */
public class ContextManager : GameBehaviour, IContextManager
{
    private StateContext<ContextManager> stateContext;
    private ICharacterController currentCharacter;

    [SerializeField]
    private GameState setupState;
    [SerializeField]
    private GameState defaultState;
    [SerializeField]
    private GameState pauseState;
    [SerializeField]
    private GameState scrumViewState;
    [SerializeField]
    private GameState planningViewState;
    [SerializeField]
    private GameState releaseState;
    [SerializeField]
    private GameState retrospectiveViewState;
    [SerializeField]
    private GameState selectedCharacterState;

    private GameState previousState;

    private void Awake()
    {
        stateContext = new StateContext<ContextManager>(this);

        // Listen to Sprint Manager.
        gameManager.Sprint.OnBeginPlanning += SwitchToPlanningView;
        gameManager.Sprint.OnBeginSprint += Default;
        gameManager.Sprint.OnRelease += SwitchToReleaseState;
        gameManager.Sprint.OnBeginRetrospective += SwitchToRetrospectiveView;

        // Listen to player controls.
        gameManager.Controls.OnEscape += EscapeCurrentState; // TODO: Just call default?
        gameManager.Controls.OnChangeView += ChangeView;
    }

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        Transition(setupState);
    }

    public void Default()
    {
        Transition(defaultState);
    }

    public void Pause()
    {
        Transition(pauseState);
    }

    public void SwitchToScrumView()
    {
        Transition(scrumViewState);
    }

    public void SwitchToPlanningView()
    {
        Transition(planningViewState);
    }

    public void SwitchToReleaseState()
    {
        Transition(releaseState);
    }

    public void SwitchToRetrospectiveView()
    {
        Transition(retrospectiveViewState);
    }

    public void SwitchToPreviousState()
    {
        Transition(previousState);
    }

    public void CharacterSelected(ICharacterController character)
    {
        DeselectCharacter();
        currentCharacter = character;
        gameManager.Interactables.EnableInteractables();
        Transition(selectedCharacterState);
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

    private void Transition(GameState nextState)
    {
        previousState = CurrentState != null ? CurrentState : null;

        stateContext.Transition(nextState);

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