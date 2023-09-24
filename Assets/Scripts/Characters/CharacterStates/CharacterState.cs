
public abstract class CharacterState : GameBehaviour, ICharacterState
{
    private ICharacterController character;

    public event Events.CharacterEvent OnHandle;
    public event Events.CharacterEvent OnExit;

    public abstract string Status { get; }

    public virtual void Handle(ICharacterController controller)
    {
        character = controller;
        OnHandle?.Invoke(character);
        this.enabled = true;
    }

    public virtual void Exit()
    {
        OnExit?.Invoke(character);
        this.enabled = false;
    }
}
