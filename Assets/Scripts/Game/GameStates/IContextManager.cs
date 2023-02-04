public delegate void OnEnableInteractables();
public delegate void OnDisableInteractables();
public delegate void OnStateTransitioned(IGameState state);

public interface IContextManager : IController
{
    public event OnEnableInteractables onEnableInteractables;

    public event OnDisableInteractables onDisableInteractables;

    public void Default();

    public void SwitchToScrumView();

    public void SwitchToPlanningView();

    public void CharacterSelected(ICharacterController character);

    public void ChangeView();

    public void EscapeCurrentState();

    public void EnableInteractables();

    public void DisableInteractables();

    public void Exit();

    public GameState CurrentState { get; }

    public ICharacterController CurrentCharacter { get; }
}
