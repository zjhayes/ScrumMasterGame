using System.Collections.Generic;
using UnityEngine;

/***
     A Station is an object which requires the character(s) to be stationary in a specific "seat".
     OnSit and OnStand are called by the Chair interactable.
***/
public abstract class Station : Interactable
{
    [SerializeField]
    protected List<Chair> chairs;

    public override void InteractWith(ICharacterController character)
    {
        FindSeat(character);
        base.InteractWith(character);
    }

    public bool HasVacancy()
    {
        return (CountOccupants() < chairs.Count);
    }

    public int CountOccupants()
    {
        int count = 0;

        foreach (Chair chair in chairs)
        {
            if(chair.Occupied)
            {
                count++;
            }
        }
        return count;
    }

    public List<ICharacterController> ListOccupants()
    {
        List<ICharacterController> occupants = new List<ICharacterController>();
        foreach (Chair chair in chairs)
        {
            occupants.Add(chair.Occupant);
        }
        return occupants;
    }

    // Returns true if character is able to sit.
    protected virtual void FindSeat(ICharacterController occupant)
    {
        foreach (Chair chair in chairs)
        {
            if (!chair.Occupied)
            {
                OnSit(occupant, chair); // Character found a chair.
                return;
            }
        }
        occupant.Frustrated(); // Else, unable to sit.
        return;
    }

    protected virtual void OnSit(ICharacterController occupant, Chair chair)
    {
        chair.Sit(occupant);
    }

    protected virtual void Stand(ICharacterController occupant)
    {
        foreach (Chair chair in chairs)
        {
            if (chair.Occupied && chair.Occupant == occupant)
            {
                chair.Stand();
                OnStand(occupant);
            }
        }
    }

    protected virtual void OnStand(ICharacterController occupant)
    {
        return; // Called after character stands.
    }

    protected void DismissAll()
    {
        foreach (Chair chair in chairs)
        {
            chair.Occupant?.FindSomethingToDo();
            chair.Stand();
        }
    }
}
