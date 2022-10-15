using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public delegate void OnInteract(CharacterController invoker);
    public OnInteract onInteract;

    public virtual void Interact(CharacterController invoker)
    {
        onInteract?.Invoke(invoker);
    }
}
