using HierarchicalStateMachine;
using System.Collections.Generic;

/* Controls the game state. */
public class ContextManager : GameBehaviour, IContextManager, IStateMachine
{
    private Dictionary<GameStates,GameState> states;
    private ICharacterController currentCharacter;

    public event Events.GameEvent OnCharacterSelect;
    public event Events.GameEvent OnCharacterDeselect;

    private void Awake()
    {
        states = new Dictionary<GameStates, GameState>
        {
            { GameStates.PLANNING, new PlanningState(gameManager) },
            { GameStates.SCRUM, new ScrumState(gameManager) },
            { GameStates.DEFAULT_VIEW, new DefaultViewState(gameManager) },
            { GameStates.RELEASE, new ReleaseState(gameManager) },
            { GameStates.RETROSPECTIVE, new RetrospectiveState(gameManager) },
            { GameStates.BOARD_VIEW, new BoardViewState(gameManager) },
            { GameStates.SELECTED_CHARACTER, new SelectedCharacterState(gameManager) },
            { GameStates.STATIC, new StaticGameState(gameManager) }
        };
    }

    private void Start()
    {
        // Listen to Sprint Manager.
        gameManager.Sprint.OnBeginPlanning += TransitionToPlanning;
        gameManager.Sprint.OnBeginSprint += TransitionToScrum;
        gameManager.Sprint.OnRelease += TransitionToRelease;
        gameManager.Sprint.OnBeginRetrospective += TransitionToRetrospective;

        InitializeGame();
    }

    private void Update()
    {
        CurrentState.Update();
    }

    public GameState GetState(GameStates stateEnum)
    {
        if (TryGetState(stateEnum, out GameState state))
        {
            return state;
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    public bool TryGetState(GameStates stateEnum, out GameState state)
    {
        return states.TryGetValue(stateEnum, out state);
    }

    public void CharacterSelected(ICharacterController character)
    {
        DeselectCharacter();
        currentCharacter = character;
        OnCharacterSelect?.Invoke();
    }

    public void DeselectCharacter()
    {
        if(currentCharacter != null)
        {
            currentCharacter.Deselect();
            currentCharacter = null;
            OnCharacterDeselect?.Invoke();
        }
    }

    public ICharacterController CurrentCharacter
    {
        get { return currentCharacter; }
        set { currentCharacter = value; }
    }

    public BaseState CurrentState { get; set; }

    private void Transition(GameStates nextStateEnum)
    {
        if(TryGetState(nextStateEnum, out GameState nextState))
        {
            CurrentState.SwitchState(nextState);
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    private void InitializeGame()
    {
        CurrentState = new SetupState(gameManager);
        CurrentState.Enter();
    }

    private void TransitionToPlanning()
    {
        Transition(GameStates.PLANNING);
    }

    private void TransitionToScrum()
    {
        Transition(GameStates.SCRUM);
    }

    private void TransitionToRelease()
    {
        Transition(GameStates.RELEASE);
    }

    private void TransitionToRetrospective()
    {
        Transition(GameStates.RETROSPECTIVE);
    }
}