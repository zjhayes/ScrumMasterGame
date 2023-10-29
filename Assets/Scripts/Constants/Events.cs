
public class Events
{
    public delegate void GameEvent();
    public delegate void PlayerEvent();
    public delegate void CharacterEvent(ICharacterController character);
    public delegate void InteractableEvent<T>(T interactable) where T : Interactable;
    public delegate void ContainerEvent(IContainable containable);
    public delegate void StoryEvent(Story story);
    public delegate void StationEvent();
    public delegate void UIEvent();
    public delegate void MenuEvent<T>(T menu) where T : MenuController;
}
