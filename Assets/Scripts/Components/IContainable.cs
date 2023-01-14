using UnityEngine;

/* Objects which can be stored in a Container gameobject. */
public interface IContainable
{
    public void OnContained(Container container);
    public void OnRemoved(Container container);

    GameObject gameObject { get; }
}
