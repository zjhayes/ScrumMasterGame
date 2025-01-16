

public class ScrumState : PausableState
{
    bool isViewingBoard = false;

    public ScrumState(IGameManager _gameManager) : base(_gameManager) {}

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
            gameManager.Context.TransitionToBoardView();
        }
        else
        {
            // Return to default view.
            gameManager.Context.TransitionToBoardView();
        }
    }

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.StateMachine.GetState(GameStates.DEFAULT_VIEW));
    }
}
