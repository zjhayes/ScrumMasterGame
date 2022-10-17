using UnityEngine;

public class ScrumMenuController : MenuController
{
    [SerializeField]
    ScrumBoardController boardController;

    public void Interact(CharacterController invoker)
    {
        boardController.CreateCartridge(invoker.GetComponent<CharacterInventory>());
        invoker.GetComponent<InteractionController>().Target = null;
    }

    public override void SetUp()
    {
        // Show on Scrum Board interactions.
        boardController.onInteract += Show;
    }
}
