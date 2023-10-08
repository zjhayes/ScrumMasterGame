using System.Collections.Generic;
using System.Linq;
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

    public IEnumerable<KeyValuePair<Interactable, int>> PrioritizeInteractablesFor(ICharacterController character)
    {
        // Get scores advertised to character by open interactables.
        Dictionary<Interactable, int> advertisements = new Dictionary<Interactable, int>();
        foreach (Interactable interactable in openInteractables)
        {
            int priorityScore = interactable.CalculatePriorityFor(character);
            advertisements.Add(interactable, priorityScore);
        }

        // Sort positive interactable advertisements by score.
        return advertisements.Where(pair => pair.Value > 0).OrderByDescending(pair => pair.Value);
    }
}
