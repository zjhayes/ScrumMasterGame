using System.Collections.Generic;
using UnityEngine;

/***
     A Station is an object which requires the player(s) to be stationary in a specific "seat".
***/
public class Station : MonoBehaviour
{
    [SerializeField]
    private List<Chair> chairs;

    void Awake()
    {
        foreach(Chair chair in chairs)
        {
            chair.onSit += Sit;
            chair.onStand += Stand;
        }
    }

    protected virtual void Sit(CharacterController occupant)
    {
        // Override 
    }

    protected virtual void Stand(CharacterController occupant)
    {
        // Override with instructions to stand.
    }
}
