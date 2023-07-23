using UnityEngine;

public class InteractableAdvertisement
{
    Interactable interactable;
    int score;

    public InteractableAdvertisement(Interactable interactable, int score)
    {
        this.interactable = interactable;
        this.score = score;
    }

    public Interactable Interactable
    {
        get { return interactable; }
        set { interactable = value; }
    }
}
