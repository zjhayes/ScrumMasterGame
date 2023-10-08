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

    public Cartridge TakeOrCreateCartridge(Transform location, Story story)
    {
        Cartridge cartridge;
        if(objectPool.TryGetFirst(out cartridge))
        {
            // Take cartridge from pool and move to desired location.
            cartridge.Story = story;
            cartridge.transform.SetPositionAndRotation(location.position, location.rotation);
            cartridge.gameObject.SetActive(true);
            return cartridge;
        }
        else
        {
            // Create new cartridge at desired location.
            GameObject cartridgeObject = BehaviourBuilder.Create(cartridgePrefab)
                .WithPosition(location.position)
                .WithRotation(location.rotation)
                .Build<Cartridge, IGameManager>(gameManager);
            cartridge = cartridgeObject.GetComponent<Cartridge>();
            cartridge.Story = story;
            return cartridgeObject.GetComponent<Cartridge>();
        }
    }
    
    public void PoolCartridge(Cartridge cartridge)
    {
        Debug.Log("Pool " + cartridge);
        // Move cartridge to pool and deactivate.
        cartridge.gameObject.SetActive(false);
        cartridge.transform.SetPositionAndRotation(transform.position, transform.rotation);
        objectPool.Add(cartridge);
    }

    public Container Pool
    {
        get { return objectPool; }
    }
}
