
using HierarchicalStateMachine;

public class ScrumState : PausableState
{
    private readonly GameState defaultViewSubState;
    private readonly GameState boardViewSubState;
    bool isViewingBoard = false;

    public ScrumState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context)
    {
        // Cache view sub-states.
        if (gameManager.Context.TryGetState(GameStates.DEFAULT_VIEW, out GameState _defaultViewState) && 
            gameManager.Context.TryGetState(GameStates.BOARD_VIEW, out GameState _boardViewState))
        {
            defaultViewSubState = _defaultViewState;
            boardViewSubState = _boardViewState;
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    public override void Enter()
    {
        gameManager.Controls.OnChangeView += ToggleBoardView;

        InitializeSubState();
        base.Enter();
    }

    public override void Exit()
    {
        gameManager.Controls.OnChangeView -= ToggleBoardView;
        base.Exit();
    }

    private void ToggleBoardView()
    {
        isViewingBoard = !isViewingBoard;
        if(isViewingBoard)
        {
            // View board.
            currentSubState.SwitchState(boardViewSubState);
        }
        else
        {
            // Return to default view.
            currentSubState.SwitchState(defaultViewSubState);
        }
    }

    private void InitializeSubState()
    {
        SetSubState(defaultViewSubState);
    }
}
