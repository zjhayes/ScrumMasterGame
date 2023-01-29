using UnityEngine;

public class BoardViewState : GameState
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
            UIManager.Instance.SelectedCharacterIcon.Hide();
            UIManager.Instance.CharacterCard.Hide();
        }

        controller.Camera.SwitchToBoardCamera();

        // Player can toggle board view.
        PlayerControls.Instance.onShowBoard += controller.Default;
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Escape Scrum Menu to default view when state changed.
        UIManager.Instance.ScrumMenu.Escape();
        PlayerControls.Instance.onShowBoard -= controller.Default;
        base.Destroy();
    }
}
