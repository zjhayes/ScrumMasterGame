
public class IdleState : CharacterState
{

    public IdleState(ICharacterController character, IGameManager gameManager) : base(character, gameManager) {}

    public override void Enter()
    {
        character.ClearTargetInteractable();
        base.Enter();
    }

    public override string Status
    {
        get { return "Idle"; }
    }
}