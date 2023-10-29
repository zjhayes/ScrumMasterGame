using HierarchicalStateMachine;

public class DefaultViewState : GameState
{

    public DefaultViewState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context) {}

    public override void Enter()
    {
        MoveCameraToOverworld();
        gameManager.Context.DeselectCharacter();
        gameManager.Context.OnCharacterSelect += SwitchToSelectedCharacterSubState;
        gameManager.Context.OnCharacterDeselect += SwitchToNoSelectedCharacterSubState;
        base.Enter();
    }

    private void MoveCameraToOverworld()
    {
        gameManager.Camera.SwitchToOverworldCamera();
        gameManager.UI.StatusBar.Show();
    }

    public override void Exit()
    {
        gameManager.Context.OnCharacterSelect -= SwitchToSelectedCharacterSubState;
        gameManager.Context.OnCharacterDeselect -= SwitchToNoSelectedCharacterSubState;
        gameManager.Context.DeselectCharacter();
        base.Exit();
    }

    private void SwitchToSelectedCharacterSubState()
    {
        SubState.SwitchState(gameManager.Context.GetState(GameStates.SELECTED_CHARACTER));
    }

    private void SwitchToNoSelectedCharacterSubState()
    {
        SubState.SwitchState(gameManager.Context.GetState(GameStates.STATIC));
    }

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.GetState(GameStates.STATIC));
    }
}

