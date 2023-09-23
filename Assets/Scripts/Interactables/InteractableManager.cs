using System.Collections.Generic;
using UnityEngine;

/* Controls the global interactability of Interactable objects. */
public class InteractableManager : MonoBehaviour
{
    private List<Interactable> openInteractables; // Interactables which character can choose from on their own.
    
    public event Events.GameEvent OnEnableInteractables;
    public event Events.GameEvent OnDisableInteractables;

    private void Awake()
    {
        openInteractables = new List<Interactable>();
    }
    
    public void EnableInteractables()
    {
        OnEnableInteractables?.Invoke();
    }

    public void DisableInteractables()
    {
        OnDisableInteractables?.Invoke();
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
