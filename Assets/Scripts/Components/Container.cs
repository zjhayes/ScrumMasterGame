using System.Collections.Generic;
using UnityEngine;

// A Container treats a GameObject as storage for other GameObjects.
public class Container : MonoBehaviour
{
    public List<GameObject> Get(string tag)
    {
        List<GameObject> containables = new List<GameObject>();
        foreach (Transform containable in transform)
        {
            if (containable.tag == tag)
            {
                containables.Add(containable.gameObject);
            }
        }
        return containables;
    }

    public IContainable[] Get<T>(bool includeInactive) where T : IContainable
    {
        return gameObject.GetComponentsInChildren(typeof(T), includeInactive) as IContainable[];
    }

    public IContainable GetFirst<T>(bool includeInactive) where T : IContainable
    {
        return gameObject.GetComponentInChildren(typeof(T), includeInactive) as IContainable;
    }

    // You can first Get the containable, then pass it to Remove.
    public void Remove(IContainable containable)
    {
        containable.gameObject.transform.parent = null;
        containable.OnRemoved(this);

    }

    // Move containable to another container, doesn't call OnContained or OnRemoved.
    public void MoveTo(IContainable containable, Container other)
    {
        if(Contains(containable))
        {
            containable.gameObject.transform.parent = other.gameObject.transform;
        }
    }

    public void Add(IContainable containable)
    {
        containable.gameObject.transform.parent = gameObject.transform;
        containable.OnContained(this);
    }

    // Checks if container currently contains provided containable.
    public bool Contains(IContainable containable)
    {
        return containable.gameObject.transform.parent == this.gameObject;
    }

    public bool IsEmpty
    {
        get { return transform.childCount <= 0; }
    }
}
