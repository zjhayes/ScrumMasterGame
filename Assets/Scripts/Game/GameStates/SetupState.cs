
public class SetupState : GameState
{
    public SetupState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
        // Initialize game.
        gameManager.Board.Initialize();
        gameManager.Sprint.BeginPlanning(); // Transition immediately to Planning.
    }
}
