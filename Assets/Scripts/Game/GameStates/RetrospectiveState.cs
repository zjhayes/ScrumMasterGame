using HierarchicalStateMachine;

public class RetrospectiveState : PausableState
{
    private readonly GameState boardViewSubState;

    public RetrospectiveState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context) 
    {
        // Cache board view sub-state and scrum transition state.
        if (gameManager.Context.TryGetState(GameStates.BOARD_VIEW, out GameState _boardViewState))
        {
            boardViewSubState = _boardViewState;
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    public override void Enter()
    {
        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.RetrospectiveMenu.Show();
        
        InitializeSubState();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        // Show scrum board when state changed. // TODO: Delete?
        gameManager.UI.RetrospectiveMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
    }

    private void InitializeSubState()
    {
        SetSubState(boardViewSubState);
    }
}
