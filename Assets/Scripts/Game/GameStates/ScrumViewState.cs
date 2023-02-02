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
            controller.GameManager.UI.SelectedCharacterIcon.Hide();
            controller.GameManager.UI.CharacterCard.Hide();
        }

        controller.GameManager.Camera.SwitchToBoardCamera();
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Escape Scrum Menu to default view when state changed.
        controller.GameManager.UI.ScrumMenu.Escape();
        base.Destroy();
    }
}
