using UnityEngine;

public class SelectedCharacterState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.EnableInteractables(); // TODO: This depends on character status.

        controller.CurrentCharacter.StateContext.onTransition += onCharacterStateChange;
        
        UIManager.Instance.SelectedCharacterIcon.Show();
        UIManager.Instance.CharacterCard.Show(controller.CurrentCharacter);

        //controller.Camera.SwitchToFollowCamera(controller.CurrentCharacter.gameObject.transform);
        controller.Camera.SwitchToOverworldCamera(); // TODO: Replace with follow camera.
    }

    public override void Escape()
    {
        controller.Default();
    }

    void onCharacterStateChange()
    {
        UIManager.Instance.CharacterCard.UpdateStatus(controller.CurrentCharacter);
    }

    public override void Destroy()
    {
        // Deselect character when state changed.
        controller.CurrentCharacter = null;
        controller.DisableInteractables();
        UIManager.Instance.SelectedCharacterIcon.Hide();
        UIManager.Instance.CharacterCard.Hide();
        base.Destroy();
    }
}