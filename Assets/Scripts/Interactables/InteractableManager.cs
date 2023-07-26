using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    [SerializeField]
    Interactable scrumBoard;
    [SerializeField]
    List<WorkStation> workStations;
    [SerializeField]
    CertificationStation certificationStation;

    List<Interactable> openInteractables;

    public delegate void OnEnableInteractables();
    public event OnEnableInteractables onEnableInteractables;
    public delegate void OnDisableInteractables();
    public event OnDisableInteractables onDisableInteractables;

    void Awake()
    {
        openInteractables = new List<Interactable>();
    }

    public void EnableInteractables()
    {
        onEnableInteractables?.Invoke();
    }

    public void DisableInteractables()
    {
        onDisableInteractables?.Invoke();
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

    // Enable interactable to advertise itself to characters.
    public void AddOpenInteractable(Interactable interactable)
    {
        if (!openInteractables.Contains(interactable))
        {
            openInteractables.Add(interactable);
        }
    }

    // Disable interactable from advertising itself to characters.
    public void RemoveOpenInteractable(Interactable interactable)
    {
        openInteractables.Remove(interactable);
    }

    public List<Interactable> OpenInteractables
    {
        get { return openInteractables; }
    }
}
