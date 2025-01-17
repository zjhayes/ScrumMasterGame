
using HierarchicalStateMachine;

public abstract class CharacterState : BaseState<CharacterState>
{
    protected ICharacterController character;
    protected IGameManager gameManager;

    public abstract string Status { get; }

    public CharacterState(ICharacterController character, IGameManager gameManager) : base()
    {
        this.character = character;
        this.gameManager = gameManager;
    }

    protected override void InitializeSubState() { }
}
