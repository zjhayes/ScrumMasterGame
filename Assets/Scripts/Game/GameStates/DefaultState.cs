using UnityEngine;

public class DefaultState : GameState
{ 
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.CurrentCharacter = null;
        controller.DisableInteractables();
        UIManager.Instance.SelectedCharacterIcon.Hide();
        UIManager.Instance.CharacterCard.Hide();

        controller.Camera.SwitchToOverworldCamera();
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }
}