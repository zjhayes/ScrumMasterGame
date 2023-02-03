using UnityEngine;

public class ScrumViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        // Deselect current character.
        if(controller.CurrentCharacter != null)
        {
            controller.CurrentCharacter = null;
            controller.DisableInteractables();
            gameManager.UI.SelectedCharacterIcon.Hide();
            gameManager.UI.CharacterCard.Hide();
        }

        controller.GameManager.Camera.SwitchToBoardCamera();
        base.Handle(controller);
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Escape Scrum Menu to default view when state changed.
        gameManager.UI.ScrumMenu.Escape();
        base.Destroy();
    }
}
