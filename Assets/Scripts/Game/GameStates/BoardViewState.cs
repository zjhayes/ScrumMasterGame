using HierarchicalStateMachine;

public class BoardViewState : GameState
{
    public BoardViewState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
        MoveCameraToBoard();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        gameManager.UI.ScrumMenu.Escape(); // Reset scrum board.
    }

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.GetState(GameStates.STATIC));
    }

    private void MoveCameraToBoard()
    {
        gameManager.UI.StatusBar.Hide();
        gameManager.Camera.SwitchToBoardCamera();
    }
}
