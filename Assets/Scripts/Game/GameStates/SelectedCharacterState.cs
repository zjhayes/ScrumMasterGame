using UnityEngine;

public class SelectedCharacterState : GameState
{
    private ContextManager controller;
    private ICharacterController selectedCharacter;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.EnableInteractables(); // TODO: This depends on character status.

        // Listen to character and update status when state changed.
        selectedCharacter = controller.CurrentCharacter;
        selectedCharacter.StateContext.onTransition += onCharacterStateChange;

        gameManager.UI.SelectedCharacterIcon.Show();
        gameManager.UI.CharacterCard.Show(selectedCharacter);

        gameManager.Camera.SwitchToOverworldCamera(); // TODO: Replace with follow camera.
        base.Handle(controller);
    }

    public override void Escape()
    {
        controller.Default();
    }

    void onCharacterStateChange()
    {
        gameManager.UI.CharacterCard.UpdateStatus(selectedCharacter);
    }

    public override void Destroy()
    {
        // Stop listening to character.
        selectedCharacter.StateContext.onTransition -= onCharacterStateChange;

        // Revert state.
        controller.DisableInteractables();
        gameManager.UI.SelectedCharacterIcon.Hide();
        gameManager.UI.CharacterCard.Hide();

        base.Destroy();
    }
}