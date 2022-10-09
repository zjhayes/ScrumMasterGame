using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Cartridge : Pickup
{
    private Interactable interaction;

    void Awake()
    {
        interaction = GetComponent<Interactable>();
    }

    void Start()
    {
        interaction.onInteract += OnInteract;
    }

    private void OnInteract(CharacterInteraction invoker)
    {
        PickUp(invoker.gameObject.GetComponent<CharacterInventory>());
    }
}
