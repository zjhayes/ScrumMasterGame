using System.Collections.Generic;
using UnityEngine;

/***
     A Station is an object which requires the player(s) to be stationary in a specific "seat".
     OnSit and OnStand are called by the Chair interactable.
***/
public class Station : MonoBehaviour
{
    [SerializeField]
    protected List<Chair> chairs;

    void Awake()
    {
        foreach(Chair chair in chairs)
        {
            chair.onSit += OnSit;
            chair.onStand += OnStand;
        }
    }

    protected virtual void OnSit(CharacterController occupant)
    {
        // Override with logic for when player joins station.
    }

    protected virtual void OnStand(CharacterController occupant)
    {
        // Override with logic for when player leaves station.
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
