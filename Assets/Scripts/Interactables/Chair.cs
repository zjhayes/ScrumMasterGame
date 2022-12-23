using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    Transform seat;

    Vector3 originalLocation;

    CharacterController occupant;

    public delegate void OnSit(CharacterController occupant);
    public OnSit onSit;

    public delegate void OnStand(CharacterController occupant);
    public OnStand onStand;

    public void Interact(CharacterController invoker)
    {
        
        if(!Occupied)
        {
            occupant = invoker;
            Sit();
        }
        else if(occupant == invoker)
        {
            Stand();
        }
    }

    private void Sit()
    {
        onSit?.Invoke(occupant);
        
        // Disable character physics.
        occupant.EnablePhysics(false);
        occupant.GetComponent<CharacterMovement>().enabled = false;

        // Lock player interaction to this.
        occupant.GetComponent<Awareness>().enabled = false;
        //occupant.GetComponent<InteractionController>().Target = this;

        // Move to seat.
        originalLocation = new Vector3();
        originalLocation.x = occupant.transform.position.x;
        originalLocation.y = occupant.transform.position.y;
        originalLocation.z = occupant.transform.position.z;
        occupant.transform.parent = seat;
        occupant.transform.position = seat.position;
        occupant.transform.rotation = seat.rotation;
    }

    private void Stand()
    {
        onStand?.Invoke(occupant);

        // Enable character physics.
        occupant.EnablePhysics(true);
        occupant.GetComponent<CharacterMovement>().enabled = true;

        // Unlock player interaction.
        occupant.GetComponent<Awareness>().enabled = true;
        occupant.GetComponent<InteractionController>().Target = null;

        // Move to original location.
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
