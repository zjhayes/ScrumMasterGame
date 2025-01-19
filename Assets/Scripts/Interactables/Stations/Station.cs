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

    protected override void Start()
    {
        // Listen for when character sits or stands.
        foreach (Chair chair in chairs)
        {
            chair.OnStand += OnChairUnoccupied;
            chair.OnSit += OnChairOccupied;
        }
        base.Start();
    }

    public override void InteractWith(ICharacterController character)
    {
        FindSeat(character);
        base.InteractWith(character);
    }

    public override bool CanInteract(ICharacterController character)
    {
        return HasVacancy(); // Can interact when available chairs.
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

    protected virtual void FindSeat(ICharacterController occupant)
    {
        foreach (Chair chair in chairs)
        {
            if(chair.TrySit(occupant))
            {
                return;
            }
        }
        
        // Else, unable to sit.
        occupant.Frustrated();
        return;
    }

    /*
     * Called when characters are dismissed by station.
     */

    protected void DismissAll()
    {
        foreach (Chair chair in chairs)
        {
            if(chair.TryStand(out ICharacterController occupant))
            {
                OnCharacterDismiss(occupant);
            }
        }
    }

    protected abstract void OnCharacterDismiss(ICharacterController occupant);

    /*
     * Called regardless of whether station or player directed character to sit/stand 
     */

    protected abstract void OnChairOccupied(ICharacterController occupant);
    protected abstract void OnChairUnoccupied(ICharacterController occupant);
}
