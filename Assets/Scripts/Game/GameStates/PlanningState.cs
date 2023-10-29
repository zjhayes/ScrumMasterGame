using HierarchicalStateMachine;

public class PlanningState : PausableState
{
    private readonly GameState boardViewSubState;

    public PlanningState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context)
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
        InitializeSubState();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        // Show scrum board when state changed.
        gameManager.UI.PlanningMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
        gameManager.UI.StatusBar.Show();
    }

    private void InitializeSubState()
    {
        SetSubState(boardViewSubState);
    }
}
