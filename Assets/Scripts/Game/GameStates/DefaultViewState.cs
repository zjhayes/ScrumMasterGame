
public class DefaultViewState : GameState
{
    GameState defaultSubState;
    GameState selectedCharacterState;

    public DefaultViewState(IGameManager gameManager) : base(gameManager) {}

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
        SwitchSubState(selectedCharacterState);
    }

    private void SwitchToNoSelectedCharacterSubState()
    {
        SwitchSubState(defaultSubState);
    }

    protected override void InitializeSubState()
    {
        defaultSubState = gameManager.Context.StateMachine.GetState(GameStates.STATIC);
        selectedCharacterState = gameManager.Context.StateMachine.GetState(GameStates.SELECTED_CHARACTER);
        SetSubState(defaultSubState);
    }
}

