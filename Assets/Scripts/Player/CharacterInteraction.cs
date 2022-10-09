using UnityEngine;

[RequireComponent(typeof(Awareness))]
public class CharacterInteraction : MonoBehaviour
{
    private Awareness awareness;
    private GameObject target;
    
    void Awake()
    {
        awareness = GetComponent<Awareness>();
    }

    public void Interact()
    {
        if(awareness.HasTarget() && awareness.Target.tag == Tags.INTERACTABLE)
        {
            awareness.Target.GetComponent<Interactable>()?.Interact(this);
        }
    }
}
