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
        OnSit(character);
        base.InteractWith(character);
    }

    protected virtual void OnSit(ICharacterController occupant)
    {
        foreach (Chair chair in chairs)
        {
            if(!chair.Occupied)
            {
                chair.Sit(occupant);
                return; // Character found a chair.
            }
        }

        // Character can't interact.
        occupant.Frustrated();
    }

    protected virtual void OnStand(ICharacterController occupant)
    {
        foreach(Chair chair in chairs)
        {
            if(chair.Occupied && chair.Occupant == occupant)
            {
                chair.Stand();
            }
        }
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
}
