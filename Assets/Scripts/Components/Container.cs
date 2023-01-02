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

    public IContainable Get<T>(bool includeInactive) where T : IContainable
    {
        return gameObject.GetComponentInChildren(typeof(T), includeInactive) as IContainable;
    }

    public void Add(IContainable containable)
    {
        // Move containable to container.
        containable.EnablePhysics(false);
        containable.gameObject.transform.parent = gameObject.transform;
        containable.gameObject.transform.position = gameObject.transform.position;
        containable.gameObject.transform.localEulerAngles = containable.ContainerRotation;
    }

    public bool IsEmpty
    {
        get { return transform.childCount <= 0; }
    }
}
