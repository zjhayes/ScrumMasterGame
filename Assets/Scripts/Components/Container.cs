using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// A Container treats a GameObject as storage for other GameObjects.
public class Container : MonoBehaviour
{
    const bool INCLUDE_INACTIVE_DEFAULT = true;

    public delegate void OnAdd();
    public event OnAdd onAdd;

    public List<GameObject> Get(string tag)
    {
        List<GameObject> containables = new List<GameObject>();
        foreach (Transform containable in transform)
        {
            if (containable.CompareTag(tag))
            {
                containables.Add(containable.gameObject);
            }
        }
        return containables;
    }

    public List<T> Get<T>(bool includeInactive = INCLUDE_INACTIVE_DEFAULT) where T : IContainable
    {
        List<T> containables = new List<T>();

        foreach(Component component in gameObject.GetComponentsInChildren(typeof(T), includeInactive))
        {
            containables.Add(component.GetComponent<T>());
        }
        return containables;
        //return gameObject.GetComponentsInChildren(typeof(T), includeInactive) as IContainable[];
    }

    public IContainable GetFirst<T>(bool includeInactive = INCLUDE_INACTIVE_DEFAULT) where T : IContainable
    {
        return gameObject.GetComponentInChildren(typeof(T), includeInactive) as IContainable;
    }

    public bool TryGetFirst<T>(out T containable, bool includeInactive = INCLUDE_INACTIVE_DEFAULT) where T : IContainable
    {
        containable = gameObject.GetComponentInChildren<T>(includeInactive);
        return containable != null;
    }

    // You can first Get the containable, then pass it to Remove.
    public void Remove(IContainable containable)
    {
        containable.gameObject.transform.parent = null;
    }

    public void Add(IContainable containable)
    {
        containable.gameObject.transform.SetParent(gameObject.transform);
        onAdd?.Invoke();
    }

    // Checks if container currently contains provided containable.
    public bool Contains(IContainable containable)
    {
        return containable.gameObject.transform.parent == this.gameObject;
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
