using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;

    private UnityEngine.AI.NavMeshAgent agent;

    void Awake()
    {
        if(inventory == null)
        {
            Debug.Log(string.Format("Character {0} does not have an inventory.", gameObject));
        }

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void PickUp(Pickup pickup)
    {
        if(HasPickup())
        {
            Drop();
        }

        // Disable pickup physics.
        pickup.GetComponent<Rigidbody>().useGravity = false;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        pickup.GetComponent<Collider>().enabled = false;

        // Move to inventory position.
        pickup.transform.parent = inventory.transform;
        pickup.transform.position = inventory.transform.position;
        pickup.transform.localEulerAngles = pickup.HoldRotation;
        
    }

    public void Drop()
    {
        foreach(Transform pickup in inventory.transform)
        {
            // Enable pickup physics.
            pickup.GetComponent<Rigidbody>().useGravity = true;
            pickup.GetComponent<Rigidbody>().isKinematic = false;
            pickup.GetComponent<Collider>().enabled = true;

            // Move out of inventory.
            pickup.transform.parent = null;
            Vector3 test = agent.velocity;
            pickup.GetComponent<Rigidbody>().AddForce(test);
            Debug.Log(agent.velocity);

        }
    }

    public bool HasPickup()
    {
        if(inventory.transform.childCount > 0)
        {
            return true;
        }
        return false;
    }
}
