using UnityEngine;

/* Contains a socketable object to a transform. Tracks removal. */
public class Socket : Container
{
    protected ISocketable currentObject;

    public event Events.ContainerEvent OnRemove;

    public bool TryPut<T>(T socketable) where T : ISocketable
    {
        if (currentObject == null)
        {
            currentObject = socketable;

            // Restrain socketable to current transform.
            currentObject.EnablePhysics(false);
            currentObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
            Add(currentObject);

            return true;
        }
        else
        {
            return false; // Already has socketable.
        }
    }

    public bool TryGet<T>(out T outObject) where T : ISocketable
    {
        outObject = (T) currentObject;
        return outObject != null;
    }

    public bool Has<T>() where T : ISocketable
    {
        return currentObject != null && currentObject is T;
    }

    protected virtual void ObjectRemovedAsChild()
    {
        ISocketable previousObject = currentObject;
        currentObject = default;
        OnRemove?.Invoke(previousObject);
    }

    // Listen for when object is removed as child of container.
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount <= 0)
        {
            ObjectRemovedAsChild();
        }
    }
}
