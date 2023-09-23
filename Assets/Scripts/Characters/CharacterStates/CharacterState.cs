
public abstract class CharacterState : GameBehaviour, ICharacterState
{
    public event Events.CharacterEvent OnHandle;
    public event Events.CharacterEvent OnExit;

    public abstract string Status { get; }

    public virtual void Handle(ICharacterController controller)
    {
        OnHandle?.Invoke();
        this.enabled = true;
    }

    public virtual void Exit()
    {
        OnExit?.Invoke();
        this.enabled = false;
    }
}
