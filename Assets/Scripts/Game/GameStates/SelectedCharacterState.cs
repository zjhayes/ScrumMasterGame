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

        GameManager.Instance.UI.SelectedCharacterIcon.Show();
        GameManager.Instance.UI.CharacterCard.Show(selectedCharacter);

        GameManager.Instance.Camera.SwitchToOverworldCamera(); // TODO: Replace with follow camera.
    }

    public override void Escape()
    {
        controller.Default();
    }

    void onCharacterStateChange()
    {
        GameManager.Instance.UI.CharacterCard.UpdateStatus(selectedCharacter);
    }

    public override void Destroy()
    {
        // Stop listening to character.
        selectedCharacter.StateContext.onTransition -= onCharacterStateChange;

        // Revert state.
        controller.DisableInteractables();
        GameManager.Instance.UI.SelectedCharacterIcon.Hide();
        GameManager.Instance.UI.CharacterCard.Hide();

        base.Destroy();
    }
}