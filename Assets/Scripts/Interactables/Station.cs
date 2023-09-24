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
        foreach(Chair chair in chairs)
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
        return HasVacancy();
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

    // Returns true if character is able to sit.
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

    protected void DismissAll()
    {
        foreach (Chair chair in chairs)
        {
            if(chair.TryStand(out ICharacterController occupant))
            {
                continue;
            }
        }
    }

    protected virtual void OnChairOccupied(ICharacterController occupant)
    {
        // Override with logic for when a character successfully sits.
    }

    protected virtual void OnChairUnoccupied(ICharacterController occupant)
    {
        occupant.FindSomethingToDo();
    }
}
