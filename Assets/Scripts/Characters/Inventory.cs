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

        // Move to inventory.
        inventory.Add(pickup);
        pickup.EnablePhysics(false);
        pickup.SetPositionToContainer(inventory);
        pickup.SetToHoldRotation();
    }

    public Pickup Drop()
    {
        Pickup pickup = CurrentPickup;
        inventory.Remove(pickup);
        pickup.ClaimedBy = null;
        pickup.EnablePhysics(true);
        return pickup;
    }

    public bool HasPickup()
    {
        if(!inventory.IsEmpty)
        {
            return true;
        }
        return false;
    }

    public Pickup CurrentPickup
    {
        get { return inventory.GetFirst<Pickup>(true) as Pickup; }
    }
}
