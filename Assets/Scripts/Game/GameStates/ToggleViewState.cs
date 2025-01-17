

public class ToggleViewState : GameState
{
    private bool isViewingBoard = false;
    private GameState defaultView;
    private GameState boardView;

    public ToggleViewState(IGameManager _gameManager) : base(_gameManager) { }

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
        if (isViewingBoard)
        {
            // View board.
            TransitionToBoardView();
        }
        else
        {
            // Return to default view.
            TransitionToDefaultView();
        }
    }

    private void TransitionToBoardView()
    {
        SwitchSubState(boardView);
    }

    private void TransitionToDefaultView()
    {
        SwitchSubState(defaultView);
    }

    protected override void InitializeSubState()
    {
        defaultView = gameManager.Context.StateMachine.GetState(GameStates.DEFAULT_VIEW);
        boardView = gameManager.Context.StateMachine.GetState(GameStates.BOARD_VIEW);
        SetSubState(defaultView);
    }
}
