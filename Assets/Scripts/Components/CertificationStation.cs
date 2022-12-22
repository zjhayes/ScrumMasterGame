using UnityEngine;

public class CertificationStation : Station
{
    [SerializeField]
    GameObject closedBook;
    [SerializeField]
    GameObject openBook;

    protected override void OnSit(CharacterController occupant)
    {
        base.OnSit(occupant);

        if (CountOccupants() == 1)
        { // This is the first player to sit.
            OnFirstOccupant();
        }

        if (occupant.Inventory.HasPickup())
        {
            occupant.Inventory.Drop();
        }
    }

    protected override void OnStand(CharacterController occupant)
    {
        base.OnStand(occupant);
        
        if (CountOccupants() == 1)
        { // This is the last occupant.
            OnUnoccupied();
        }
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
