using System.Collections.Generic;
using UnityEngine;

/* Controls the global interactability of Interactable objects. */
public class InteractableManager : MonoBehaviour
{
    private List<Interactable> openInteractables; // Interactables which character can choose from on their own.
    
    public delegate void OnEnableInteractables();
    public event OnEnableInteractables onEnableInteractables;
    public delegate void OnDisableInteractables();
    public event OnDisableInteractables onDisableInteractables;

    private void Awake()
    {
        openInteractables = new List<Interactable>();
    }
    
    public void EnableInteractables()
    {
        onEnableInteractables?.Invoke();
    }

    public void DisableInteractables()
    {
        onDisableInteractables?.Invoke();
    }

    // Enable interactable to advertise itself to characters.
    public void AddOpenInteractable(Interactable interactable)
    {
        if (!openInteractables.Contains(interactable))
        {
            openInteractables.Add(interactable);
        }
    }

    // Disable interactable from advertising itself to characters.
    public void RemoveOpenInteractable(Interactable interactable)
    {
        openInteractables.Remove(interactable);
    }

    public List<Interactable> OpenInteractables
    {
        get { return openInteractables; }
    }
}
