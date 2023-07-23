using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField]
    Interactable scrumBoard;
    [SerializeField]
    List<WorkStation> workStations;
    [SerializeField]
    CertificationStation certificationStation;

    public delegate void OnEnableInteractables();
    public event OnEnableInteractables onEnableInteractables;
    public delegate void OnDisableInteractables();
    public event OnDisableInteractables onDisableInteractables;
    public delegate void OnAdvertiseInteractable();
    public event OnAdvertiseInteractable onAdvertiseInteractable;

    public void EnableInteractables()
    {
        onEnableInteractables?.Invoke();
    }

    public void DisableInteractables()
    {
        onDisableInteractables?.Invoke();
    }

    public WorkStation FindOpenWorkStation()
    {
        foreach(WorkStation workStation in workStations)
        {
            if(workStation.CountOccupants() <= 0 && workStation.ClaimedBy == null)
            {
                return workStation;
            }
        }
        return null;
    }

    public WorkStation FindPairProgrammingStation()
    {
        foreach (WorkStation workStation in workStations)
        {
            if (workStation.CountOccupants() == 1)
            {
                // This station has room for a pair programmer.
                return workStation;
            }
        }
        return null;
    }

    public List<WorkStation> WorkStations
    {
        get { return workStations; }
    }

    public CertificationStation CertificationStation
    {
        get { return certificationStation; }
    }

    public Interactable ScrumBoard
    {
        get { return scrumBoard; }
    }
}
