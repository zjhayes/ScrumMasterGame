using UnityEngine;

public class CertificationStation : Station
{
    [SerializeField]
    private GameObject closedBook;
    [SerializeField]
    private GameObject openBook;

    public event Events.CharacterEvent OnFirstOccupant;
    public event Events.CharacterEvent OnUnoccupied;

    protected override void OnChairOccupied(ICharacterController occupant)
    {
        if (CountOccupants() == 1)
        { // This is the first character to sit.
            FirstOccupant(occupant);
        }
    }

    protected override void OnChairUnoccupied(ICharacterController occupant)
    {
        if (CountOccupants() <= 0)
        { // This is the last occupant.
            Unoccupied(occupant);
        }
    }

    protected override void OnCharacterDismiss(ICharacterController occupant)
    {
        occupant.FindSomethingToDo();
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        // If character has free time and good management skills, they're more likely to use this station.
        if(character.Stats.TimeManagement > 2) // TODO: Scale score with time management stat
        {
            return PriorityScore.CERTIFICATION_STATION;
        }
        return PriorityScore.NO_SCORE;
    }

    private void FirstOccupant(ICharacterController firstOccupant)
    {
        OpenBook();
        OnFirstOccupant?.Invoke(firstOccupant);
    }

    private void Unoccupied(ICharacterController lastOccupant)
    {
        CloseBook();
        OnUnoccupied?.Invoke(lastOccupant);
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
