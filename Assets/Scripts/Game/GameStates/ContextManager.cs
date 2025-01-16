
public class ContextManager : GameBehaviour
{
    private GameContext context;
    private ICharacterController currentCharacter;

    public event Events.GameEvent OnCharacterSelect;
    public event Events.GameEvent OnCharacterDeselect;

    private void Awake()
    {
        context = new GameContext(gameManager);
    }

    private void Start()
    {
        // Listen to Sprint Manager.
        gameManager.Sprint.OnBeginPlanning += TransitionToPlanning;
        gameManager.Sprint.OnBeginSprint += TransitionToScrum;
        gameManager.Sprint.OnRelease += TransitionToRelease;
        gameManager.Sprint.OnBeginRetrospective += TransitionToRetrospective;
        context.Start();
    }

    private void Update()
    {
        context.CurrentState.Update();
    }

    private void TransitionToPlanning()
    {
        context.TransitionTo(GameStates.PLANNING);
    }

    private void TransitionToScrum()
    {
        context.TransitionTo(GameStates.SCRUM);
    }

    private void TransitionToRelease()
    {
        context.TransitionTo(GameStates.RELEASE);
    }

    private void TransitionToRetrospective()
    {
        context.TransitionTo(GameStates.RETROSPECTIVE);
    }

    public void TransitionToBoardView()
    {
        context.CurrentState.SetSubState(gameManager.Context.StateMachine.GetState(GameStates.BOARD_VIEW));
    }

    public void TransitionToDefaultView()
    {
        context.CurrentState.SetSubState(gameManager.Context.StateMachine.GetState(GameStates.DEFAULT_VIEW));
    }

    public void CharacterSelected(ICharacterController character)
    {
        DeselectCharacter();
        currentCharacter = character;
        OnCharacterSelect?.Invoke();
    }

    public void DeselectCharacter()
    {
        if (currentCharacter != null)
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

    public GameContext StateMachine
    {
        get { return context; }
    }
}
