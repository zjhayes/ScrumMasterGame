using UnityEngine;

// Instantiate prefabs with injectable service.
public class BehaviourBuilder
{
    GameObject prefab;
    Vector3 position;
    Quaternion rotation;
    Transform parent;

    private BehaviourBuilder(GameObject prefab)
    {
        this.prefab = prefab;
        // Initialize default settings.
        position = Vector3.zero;
        rotation = Quaternion.identity;
        parent = null;
    }

    public static BehaviourBuilder Create(GameObject prefab)
    {
        return new BehaviourBuilder(prefab);
    }

    public BehaviourBuilder WithPosition(Vector3 position)
    {
        this.position = position;
        return this;
    }

    public BehaviourBuilder WithRotation(Quaternion rotation)
    {
        this.rotation = rotation;
        return this;
    }

    public BehaviourBuilder WithParent(Transform parent)
    {
        this.parent = parent;
        return this;
    }

    // Finalize new behaviour and inject service.
    public GameObject Build<B, S>(S service)
        where B : IBehaviour<S>
        where S : IService
    {
        prefab.SetActive(false); // Disable before instantiating.
        GameObject gameObject = GameObject.Instantiate(prefab, position, rotation, parent);
        B behaviour = gameObject.GetComponent<B>();
        behaviour.Inject(service);
        gameObject.SetActive(true); // Enable new behaviour.
        prefab.SetActive(true); // Re-enable prefab.
        return gameObject;
    }
}
