using UnityEngine;

public class Events : MonoBehaviour
{
    public delegate void PlayerEvent();
    public delegate void CharacterEvent();
    public delegate void CharacterInteractionEvent(ICharacterController controller);
    public delegate void GameEvent();
    public delegate void UIEvent();
}
