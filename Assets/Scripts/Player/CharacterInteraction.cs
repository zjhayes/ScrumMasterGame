using UnityEngine;

[RequireComponent(typeof(Awareness))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterInteraction : MonoBehaviour
{
    private Awareness awareness;
    private GameObject target;
    private CharacterInventory inventory;
    
    void Awake()
    {
        awareness = GetComponent<Awareness>();
        inventory = GetComponent<CharacterInventory>();
    }

    public void Interact()
    {
        if(awareness.HasTarget() && awareness.Target.tag == Tags.INTERACTABLE)
        {
            awareness.Target.GetComponent<Interactable>()?.Interact(this);
        }
        else if(inventory.HasPickup())
        {
            inventory.Drop();
        }
    }
}
