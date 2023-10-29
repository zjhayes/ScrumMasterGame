using HierarchicalStateMachine;
using System.Collections.Generic;

/* Controls the game state. */
public class ContextManager : GameBehaviour, IContextManager
{
    private StateMachine context;
    private Dictionary<GameStates,GameState> states;
    private ICharacterController currentCharacter;

    public event Events.GameEvent OnCharacterSelect;
    public event Events.GameEvent OnCharacterDeselect;

    private void Awake()
    {
        context = new StateMachine();
        states = new Dictionary<GameStates, GameState>
        {
            { GameStates.PLANNING, new PlanningState(gameManager, context) },
            { GameStates.SCRUM, new ScrumState(gameManager, context) },
            { GameStates.DEFAULT_VIEW, new DefaultViewState(gameManager, context) },
            { GameStates.RELEASE, new ReleaseState(gameManager, context) },
            { GameStates.RETROSPECTIVE, new RetrospectiveState(gameManager, context) },
            { GameStates.BOARD_VIEW, new BoardViewState(gameManager, context) },
            { GameStates.SELECTED_CHARACTER, new SelectedCharacterState(gameManager, context) },
            { GameStates.STATIC, new StaticGameState(gameManager, context) }
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
        context.CurrentState.Update();
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

    private void Transition(GameStates nextStateEnum)
    {
        if(TryGetState(nextStateEnum, out GameState nextState))
        {
            context.CurrentState.SwitchState(nextState);
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    private void InitializeGame()
    {
        context.CurrentState = new SetupState(gameManager, context);
        context.CurrentState.Enter();
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