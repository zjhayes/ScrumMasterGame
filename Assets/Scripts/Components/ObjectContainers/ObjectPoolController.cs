using UnityEngine;

[RequireComponent(typeof(Container))]
public class ObjectPoolController : GameBehaviour
{
    [SerializeField]
    GameObject cartridgePrefab;

    Container objectPool;

    void Awake()
    {
        objectPool = GetComponent<Container>();
    }

    public Cartridge TakeOrCreateCartridge(Transform location, Task task)
    {
        Cartridge cartridge;
        if(objectPool.TryGetFirst(out cartridge))
        {
            // Take cartridge from pool and move to desired location.
            cartridge.gameObject.SetActive(true);
            cartridge.transform.SetPositionAndRotation(location.position, location.rotation);
            cartridge.Task = task;
            return cartridge;
        }
        else
        {
            // Create new cartridge at desired location.
            GameObject cartridgeObject = BehaviourFactory.Create<Cartridge, IGameManager>(cartridgePrefab, gameManager, location.position, location.rotation);
            cartridge = cartridgeObject.GetComponent<Cartridge>();
            cartridge.Task = task;
            return cartridgeObject.GetComponent<Cartridge>();
        }
    }

    public void PoolCartridge(Cartridge cartridge)
    {
        // Move cartridge to pool and deactivate.
        cartridge.transform.SetPositionAndRotation(transform.position, transform.rotation);
        objectPool.Add(cartridge);
        cartridge.gameObject.SetActive(false);
    }

    public Container Pool
    {
        get { return objectPool; }
    }
}
