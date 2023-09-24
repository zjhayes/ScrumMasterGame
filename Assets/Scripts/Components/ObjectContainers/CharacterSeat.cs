using UnityEngine;

public class CharacterSeat : MonoBehaviour
{
    protected ICharacterController occupant;

    public event Events.CharacterEvent OnSit;
    public event Events.CharacterEvent OnStand;

    public bool Occupied
    {
        get { return occupant != null; }
    }

    public bool TrySit(ICharacterController character)
    {
        if (occupant == null)
        {
            occupant = character;
            occupant.EnablePhysics(false);
            occupant.transform.SetPositionAndRotation(transform.position, transform.rotation);
            occupant.transform.SetParent(transform);
            OnSit?.Invoke(occupant);
            return true;
        }
        else
        {
            return false; // Occupied.
        }
    }

    public bool TryGetOccupant(out ICharacterController outOccupant)
    {
        outOccupant = occupant;
        return outOccupant != null;
    }

    private void OccupantStood()
    {
        OnStand?.Invoke(occupant);
        occupant = null;
    }

    // Listen for when pickup is removed as child of container.
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount <= 0)
        {
            OccupantStood();
        }
    }
}
