using UnityEngine;

public static class BehaviourFactory
{
    // Instantiate GameObject from prefab injected with services for given behaviour.
    public static GameObject Create<B,S>(GameObject prefab, S service, Vector3 position, Quaternion rotation) 
        where B : IBehaviour<S>
        where S : IService
    {
        prefab.SetActive(false); // Disable before instantiating.
        GameObject gameObject = (GameObject)GameObject.Instantiate(prefab, position, rotation);
        B behaviour = gameObject.GetComponent<B>();
        behaviour.Inject(service);
        gameObject.SetActive(true); // Enable new behaviour.
        prefab.SetActive(true); // Re-enable prefab.
        return gameObject;
    }
}
