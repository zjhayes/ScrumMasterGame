using UnityEngine;

[RequireComponent(typeof(Awareness))]
[RequireComponent(typeof(CharacterController))]
public class InteractionController : MonoBehaviour
{
    private CharacterController controller;
    private Awareness awareness;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        awareness = GetComponent<Awareness>();
    }

    public void Interact()
    {
        if(awareness.HasTarget() && awareness.Target.tag == Tags.INTERACTABLE)
        {
            awareness.Target.GetComponent<Interactable>()?.Interact(controller);
        }
        else if(controller.Inventory.HasPickup())
        {
            // Drop inventory when nothing else to do.
            controller.Inventory.Drop();
        }
    }
}
