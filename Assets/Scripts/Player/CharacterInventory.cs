using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;

    void Awake()
    {
        if(inventory == null)
        {
            Debug.Log(string.Format("Character {0} does not have an inventory.", gameObject));
        }
    }

    public void PickUp(Pickup pickup)
    {
        pickup.GetComponent<Rigidbody>().useGravity = false;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        pickup.GetComponent<Collider>().enabled = false;
        pickup.transform.parent = inventory.transform; // Move to inventory child.
        pickup.transform.position = inventory.transform.position; // Set hold position.
        pickup.transform.localEulerAngles = pickup.HoldRotation; // Set hold rotation.
        
    }

    public void Drop()
    {
        //pickup.GetComponent<Rigidbody>().useGravity = true;
        //pickup.GetComponent<Rigidbody>().isKinematic = false;
        //pickup.GetComponent<Collider>().enabled = true;
    }
}
