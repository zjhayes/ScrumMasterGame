using UnityEngine;
using HierarchicalStateMachine;

/* Controls the game state. */
public class ContextManager : GameBehaviour, IContextManager
{
    private ICharacterController currentCharacter;

    [SerializeField]
    private GameStateDictionary states;

    private StateMachine context;

    public event Events.GameEvent OnCharacterSelect;
    public event Events.GameEvent OnCharacterDeselect;

    private void Awake()
    {
        context = new StateMachine();
        //states.Add(GameStates.PLANNING, new PlanningState(gameManager, context));
        //states.Add(GameStates.BOARD_VIEW, new BoardViewState(gameManager, context));
    }

    private void Start()
    {
        InitializeGame();

        // Listen to Sprint Manager.
        gameManager.Sprint.OnBeginPlanning += TransitionToPlanning;
        gameManager.Sprint.OnBeginSprint += TransitionToScrum;
        gameManager.Sprint.OnRelease += TransitionToRelease;
        gameManager.Sprint.OnBeginRetrospective += TransitionToRetrospective;
    }

    private void Update()
    {
        context.CurrentState.Update();
    }

    public bool TryGetState(GameStates stateEnum, out GameState state)
    {
        return states.TryGetValue(stateEnum, out state);
    }

    private void InitializeGame()
    {
        context.CurrentState = new SetupState(gameManager, context);
    }

    public void TransitionToPlanning()
    {
        Transition(GameStates.PLANNING);
    }

    public void TransitionToScrum()
    {
        Transition(GameStates.SCRUM);
    }

    public void TransitionToRelease()
    {
        Transition(GameStates.RELEASE);
    }

    public void TransitionToRetrospective()
    {
        Transition(GameStates.RETROSPECTIVE);
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
}