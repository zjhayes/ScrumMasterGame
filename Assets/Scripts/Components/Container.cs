using System.Collections.Generic;
using UnityEngine;

// A Container treats a GameObject as storage.
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

    public void Add(GameObject containable)
    {
        // Make child of Container.
        containable.transform.parent = transform;
    }

    public bool IsEmpty
    {
        get { return transform.childCount < 0; }
    }
}
