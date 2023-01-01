using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Container inventory;

    private CharacterController character;

    void Awake()
    {
        if(inventory == null)
        {
            Debug.Log(string.Format("Character {0} does not have an inventory container.", gameObject));
        }

        character = GetComponent<CharacterController>();
    }

    public void PickUp(Pickup pickup)
    {
        if(HasPickup()) // Swap pickups if one carried.
        {
            Drop();
        }

        // Disable pickup physics.
        pickup.GetComponent<Rigidbody>().useGravity = false;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        pickup.GetComponent<Collider>().enabled = false;

        // Move to inventory position.
        inventory.Add(pickup.gameObject); // Set inventory as parent.
        pickup.transform.parent = inventory.transform;
        pickup.transform.position = inventory.transform.position;
        pickup.transform.localEulerAngles = pickup.HoldRotation;
        
    }

    private void DropOnNullInteraction()
    {
        // Drop pickup when nothing else to do.
        if (HasPickup() /*&& !interaction.Target*/)
        {
            Drop();
        }
    }

    public List<Pickup> Drop()
    {
        List<Pickup> dropped = new List<Pickup>();
        foreach(GameObject pickup in inventory.Get(Tags.INTERACTABLE))
        {
            // Enable pickup physics.
            pickup.GetComponent<Rigidbody>().useGravity = true;
            pickup.GetComponent<Rigidbody>().isKinematic = false;
            pickup.GetComponent<Collider>().enabled = true;

            // Move out of inventory.
            pickup.transform.parent = null;
            //pickup.GetComponent<Rigidbody>().AddForce(character.Direction * character.Speed);

            dropped.Add(pickup.GetComponent<Pickup>());
        }
        return dropped;
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
