using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumBoardController : Interactable
{
    [SerializeField]
    private GameObject cartridgePrefab;

    public override void Interact(CharacterController invoker)
    {
        base.Interact(invoker);
        CreateCartridge(invoker.GetComponent<CharacterInventory>());
    }

    private void CreateCartridge(CharacterInventory inventory)
    {
        GameObject cartridge = (GameObject) Instantiate(cartridgePrefab, transform.position, transform.rotation);
        inventory.PickUp(cartridge.GetComponent<Cartridge>());
    }
}
