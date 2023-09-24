using UnityEngine;

public class Events : MonoBehaviour
{
    public delegate void GameEvent();
    public delegate void PlayerEvent();
    public delegate void CharacterEvent(ICharacterController character);
    public delegate void InteractableEvent<T>(T interactable) where T : Interactable;
    public delegate void ContainerEvent(IContainable containable);
    public delegate void UIEvent();
}
