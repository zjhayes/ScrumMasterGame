
using HierarchicalStateMachine;

public class SetupState : GameState
{

    public SetupState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context) {}

    public override void Enter()
    {
        // Initialize game.
        gameManager.Board.Initialize();
        gameManager.Sprint.BeginPlanning(); // Transition immediately to Planning.
    }
}
