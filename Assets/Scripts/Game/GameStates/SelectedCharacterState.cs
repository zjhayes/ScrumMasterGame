using UnityEngine;

public class SelectedCharacterState : GameState
{
    private ContextManager controller;
    private CharacterController selectedCharacter;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.EnableInteractables(); // TODO: This depends on character status.

        // Listen to character and update status when state changed.
        selectedCharacter = controller.CurrentCharacter;
        selectedCharacter.StateContext.onTransition += onCharacterStateChange;

        controller.GameManager.UI.SelectedCharacterIcon.Show();
        controller.GameManager.UI.CharacterCard.Show(selectedCharacter);

        controller.GameManager.Camera.SwitchToOverworldCamera(); // TODO: Replace with follow camera.
    }

    public override void Escape()
    {
        controller.Default();
    }

    void onCharacterStateChange()
    {
        controller.GameManager.UI.CharacterCard.UpdateStatus(selectedCharacter);
    }

    public override void Destroy()
    {
        // Stop listening to character.
        selectedCharacter.StateContext.onTransition -= onCharacterStateChange;

        // Revert state.
        controller.DisableInteractables();
        controller.GameManager.UI.SelectedCharacterIcon.Hide();
        controller.GameManager.UI.CharacterCard.Hide();

        base.Destroy();
    }
}