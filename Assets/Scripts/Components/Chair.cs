using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    private Socket seat;
    [SerializeField]
    private Transform exitToPosition;

    public event Events.CharacterEvent OnSit;
    public event Events.CharacterEvent OnStand;

    private void Start()
    {
        seat.OnAdd += InvokeSit;
        seat.OnRemove += InvokeStand;
    }

    public bool TrySit(ICharacterController newOccupant)
    {
        return seat.TryPut(newOccupant as CharacterController);
    }

    public bool TryStand(out ICharacterController dismissedOccupant)
    {
        dismissedOccupant = Stand();
        return dismissedOccupant != null;
    }

    public bool Occupied
    {
        get { return seat.Has<CharacterController>(); }
    }

    public Socket Seat
    {
        get { return seat; }
    }

    // Force character to stand.
    private ICharacterController Stand()
    {
        if(seat.TryGet(out CharacterController occupant))
        {
            occupant.transform.SetParent(null);
            occupant.EnablePhysics(true);
            occupant.transform.SetPositionAndRotation(exitToPosition.position, transform.rotation);
            return occupant;
        }
        else
        {
            return null; // Unoccupied.
        }
    }

    // Invokes OnSit when character becomes parented to seat.
    private void InvokeSit(IContainable character)
    {
        OnSit?.Invoke(character as ICharacterController);
    }

    // Invokes OnStand when character is no longer parented to seat.
    private void InvokeStand(IContainable character)
    {
        OnStand.Invoke(character as ICharacterController);
    }
}
