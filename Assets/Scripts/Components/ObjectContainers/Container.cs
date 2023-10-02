using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// A Container treats a GameObject as storage for other GameObjects.
public class Container : MonoBehaviour
{
    const bool INCLUDE_INACTIVE_DEFAULT = false;

    public event Events.ContainerEvent OnAdd;

    public List<T> Get<T>(bool includeInactive = INCLUDE_INACTIVE_DEFAULT) where T : IContainable
    {
        List<T> containables = new();

        foreach(Component component in gameObject.GetComponentsInChildren(typeof(T), includeInactive))
        {
            containables.Add(component.GetComponent<T>());
        }
        return containables;
    }

    public bool TryGetFirst<T>(out T containable, bool includeInactive = INCLUDE_INACTIVE_DEFAULT) where T : IContainable
    {
        containable = gameObject.GetComponentInChildren<T>(includeInactive);
        return containable != null;
    }

    public void Add(IContainable containable)
    {
        containable.transform.SetParent(transform);
        OnAdd?.Invoke(containable);
    }

    // Checks if container currently contains provided type.
    public bool Contains<T>(bool includeInactive = INCLUDE_INACTIVE_DEFAULT) where T : IContainable
    {
        return Get<T>(includeInactive).Any();
    }

    public bool IsEmpty
    {
        get { return transform.childCount <= 0; }
    }
}
