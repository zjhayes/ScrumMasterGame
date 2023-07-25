using UnityEngine;
using System.Collections.Generic;

public class InteractableAdvertisements
{
    List<Interactable> advertisedInteractables;
    ICharacterController advertisee;

    public InteractableAdvertisements(ICharacterController advertisee)
    {
        this.advertisee = advertisee;
        advertisedInteractables = new List<Interactable>();
    }

    public List<Interactable> Interactables { get; }
    public ICharacterController Advertisee { get; set; }
}
