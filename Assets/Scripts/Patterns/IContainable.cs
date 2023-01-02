using UnityEngine;

/* Objects which can be stored in a Container gameobject. */
public interface IContainable
{
    public void EnablePhysics(bool enable);

    GameObject gameObject { get; }
    Vector3 ContainerRotation { get; }
}
