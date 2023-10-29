
public interface IContextManager : IController
{
    public event Events.GameEvent OnCharacterSelect;
    public event Events.GameEvent OnCharacterDeselect;
    public GameState GetState(GameStates stateEnum);
    public bool TryGetState(GameStates stateEnum, out GameState state);
    public void TransitionToScrum();
    public void TransitionToPlanning();
    public void TransitionToRelease();
    public void TransitionToRetrospective();
    public void CharacterSelected(ICharacterController character);
    public void DeselectCharacter();
    public ICharacterController CurrentCharacter { get; }
}
