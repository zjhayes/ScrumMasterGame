using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    Transform seat;

    Vector3 originalLocation;

    CharacterController occupant;

    public void Sit(CharacterController _occupant)
    {
        occupant = _occupant;
        
        // Disable character physics.
        occupant.EnablePhysics(false);

        // Store original location and Move to seat.
        originalLocation = new Vector3();
        originalLocation.x = occupant.transform.position.x;
        originalLocation.y = occupant.transform.position.y;
        originalLocation.z = occupant.transform.position.z;
        occupant.transform.parent = seat;
        occupant.transform.position = seat.position;
        occupant.transform.rotation = seat.rotation;
    }

    public void Stand()
    {
        // Enable character physics.
        occupant.EnablePhysics(true);

        // Move to original location.       // TODO: Put character behind chair.
        occupant.transform.parent = null;
        occupant.transform.position = originalLocation;
        occupant = null;
    }

    public CharacterController Occupant
    {
        get { return occupant; }
    }

    public bool Occupied
    {
        get { return occupant != null; }
    }
}
