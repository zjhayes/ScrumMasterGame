using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField]
    private List<Chair> chairs;
    [SerializeField]
    private Transform cartridgeIntake;

    CharacterController developer;
    CharacterController peerReviewer;

    void Awake()
    {
        foreach(Chair chair in chairs)
        {
            chair.onSit += Sit;
            chair.onStand += Stand;
        }
    }

    private void Sit(CharacterController occupant)
    {
        if(!developer)
        {
            developer = occupant;

            if (developer.Inventory.HasPickup())
            {
                InputCartridge(developer);
            }
        }
        else if(!peerReviewer)
        {
            peerReviewer = occupant;

            if (peerReviewer.Inventory.HasPickup())
            {
                peerReviewer.Inventory.Drop();
            }
        }
    }

    private void Stand(CharacterController occupant)
    {
        if(developer == occupant)
        {
            developer = null;
        }
        else if(peerReviewer == occupant)
        {
            peerReviewer = null;
        }
    }

    private void InputCartridge(CharacterController character)
    {
        Pickup pickup = character.Inventory.Drop()[0]; // Get first pickup.

        // Disable pickup physics.
        pickup.GetComponent<Rigidbody>().useGravity = false;
        pickup.GetComponent<Rigidbody>().isKinematic = true;
        pickup.GetComponent<Collider>().enabled = false;

        // Move to intake position.
        pickup.transform.parent = cartridgeIntake;
        pickup.transform.position = cartridgeIntake.position;
        pickup.transform.rotation = cartridgeIntake.rotation;
    }
}
