using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    public delegate void OnInteract(CharacterInteraction invoker);
    public OnInteract onInteract;

    public virtual void Interact(CharacterInteraction invoker)
    {
        onInteract?.Invoke(invoker);
    }
}
