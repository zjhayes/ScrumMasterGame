using System.Collections.Generic;
using UnityEngine;

/***
     A Station is an object which requires the character(s) to be stationary in a specific "seat".
     OnSit and OnStand are called by the Chair interactable.
***/
public class Station : Interactable
{
    [SerializeField]
    protected List<Chair> chairs;

    public override void InteractWith(CharacterController character)
    {
        OnSit(character);
        base.InteractWith(character);
    }

    protected virtual void OnSit(CharacterController occupant)
    {
        foreach(Chair chair in chairs)
        {
            if(!chair.Occupied)
            {
                chair.Sit(occupant);
                return;
            }
        }
        // TODO: Handle fully occupied
    }

    protected virtual void OnStand(CharacterController occupant)
    {
        foreach(Chair chair in chairs)
        {
            if(chair.Occupied && chair.Occupant == occupant)
            {
                chair.Stand();
            }
        }
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
