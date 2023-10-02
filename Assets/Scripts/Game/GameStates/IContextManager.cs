
public interface IContextManager : IController
{
    public void Default();

    public void SwitchToScrumView();

    public void SwitchToPlanningView();
    public void SwitchToRetrospectiveView();

    public void CharacterSelected(ICharacterController character);
    public void DeselectCharacter();

    public void ChangeView();

    public void EscapeCurrentState();

    public void Exit();

    public GameState CurrentState { get; }

    public ICharacterController CurrentCharacter { get; }
}
