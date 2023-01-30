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
            GameManager.Instance.UI.SelectedCharacterIcon.Hide();
            GameManager.Instance.UI.CharacterCard.Hide();
        }

        controller.Camera.SwitchToBoardCamera();
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Escape Scrum Menu to default view when state changed.
        GameManager.Instance.UI.ScrumMenu.Escape();
        base.Destroy();
    }
}
