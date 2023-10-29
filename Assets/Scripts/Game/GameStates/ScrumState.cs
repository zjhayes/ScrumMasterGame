
using HierarchicalStateMachine;

public class ScrumState : PausableState
{
    bool isViewingBoard = false;

    public ScrumState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context) {}

    public override void Enter()
    {
        gameManager.Controls.OnChangeView += ToggleBoardView;
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
            SubState.SwitchState(gameManager.Context.GetState(GameStates.BOARD_VIEW));
        }
        else
        {
            // Return to default view.
            SubState.SwitchState(gameManager.Context.GetState(GameStates.DEFAULT_VIEW));
        }
    }

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.GetState(GameStates.DEFAULT_VIEW));
    }
}
