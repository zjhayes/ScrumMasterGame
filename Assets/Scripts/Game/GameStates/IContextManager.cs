
public interface IContextManager : IController
{
    public void Default();

    public void SwitchToScrumView();

    public void SwitchToPlanningView();
    public void SwitchToReleaseState();
    public void SwitchToRetrospectiveView();
    public void SwitchToPreviousState();

    public void CharacterSelected(ICharacterController character);
    public void DeselectCharacter();

    public void ChangeView();

    public void EscapeCurrentState();

    public GameState CurrentState { get; }

    public ICharacterController CurrentCharacter { get; }
}
