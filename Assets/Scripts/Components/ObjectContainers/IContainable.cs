using UnityEngine;

/* Objects which can be stored in a Container gameobject. */
public interface IContainable
{
    GameObject gameObject { get; }
    Transform transform { get; }
}
