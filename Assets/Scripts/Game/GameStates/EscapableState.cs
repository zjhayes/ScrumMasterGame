
using HierarchicalStateMachine;

public abstract class EscapableState : GameState
{
    protected EscapableState(IGameManager _gameManager) : base(_gameManager){}

    public override void Enter()
    {
        gameManager.Controls.OnEscape += Escape;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        gameManager.Controls.OnEscape -= Escape;
    }

    protected abstract void Escape();
}
