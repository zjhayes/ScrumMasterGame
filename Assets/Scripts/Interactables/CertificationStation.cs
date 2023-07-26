using UnityEngine;

public class CertificationStation : Station
{
    [SerializeField]
    GameObject closedBook;
    [SerializeField]
    GameObject openBook;

    protected override void OnSit(ICharacterController occupant)
    {
        if (occupant.Inventory.HasPickup())
        {
            occupant.Inventory.Drop();
        }

        base.OnSit(occupant);

        if (CountOccupants() == 1)
        { // This is the first player to sit.
            OnFirstOccupant();
        }
    }

    protected override void OnStand(ICharacterController occupant)
    {   
        if (CountOccupants() == 1)
        { // This is the last occupant.
            OnUnoccupied();
        }

        base.OnStand(occupant);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        // If character has free time and good management skills, they're more likely to use this station.
        if(character.Stats.TimeManagement > 2) // TODO: Scale score with time management stat
        {
            return PriorityScoreConstants.CERTIFICATION_STATION;
        }
        return PriorityScoreConstants.NO_SCORE;
    }

    private void OnFirstOccupant()
    {
        OpenBook();
    }

    private void OnUnoccupied()
    {
        CloseBook();
    }

    private void CloseBook()
    {
        closedBook.GetComponent<Renderer>().enabled = true;
        openBook.GetComponent<Renderer>().enabled = false;
    }

    private void OpenBook()
    {
        closedBook.GetComponent<Renderer>().enabled = false;
        openBook.GetComponent<Renderer>().enabled = true;
    }
}
