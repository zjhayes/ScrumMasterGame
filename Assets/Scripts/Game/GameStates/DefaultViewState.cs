using HierarchicalStateMachine;

public class DefaultViewState : GameState
{
    private readonly GameState noSelectedCharacterState;
    private readonly GameState selectedCharacterSubState;

    public DefaultViewState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context)
    {
        // Cache selected character state.
        if (gameManager.Context.TryGetState(GameStates.STATIC, out GameState _staticState) &&
            gameManager.Context.TryGetState(GameStates.CHARACTER_SELECTED, out GameState _selectedCharacterState))
        {
            selectedCharacterSubState = _selectedCharacterState;
            noSelectedCharacterState = _staticState;
        }
        else
        {
            throw new StateNotFoundException();
        }
    }

    public override void Enter()
    {
        MoveCameraToOverworld();
        gameManager.Context.DeselectCharacter();
        gameManager.Context.OnCharacterSelect += SwitchToSelectedCharacterSubState;
        gameManager.Context.OnCharacterDeselect += SwitchToNoSelectedCharacterSubState;
        InitializeSubState();
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
        currentSubState.SwitchState(selectedCharacterSubState);
    }

    private void SwitchToNoSelectedCharacterSubState()
    {
        currentSubState.SwitchState(noSelectedCharacterState);
    }

    private void InitializeSubState()
    {
        SetSubState(noSelectedCharacterState);
    }
}

