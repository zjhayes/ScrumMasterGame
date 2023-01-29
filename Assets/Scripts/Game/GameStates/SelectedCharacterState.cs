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
        
        UIManager.Instance.SelectedCharacterIcon.Show();
        UIManager.Instance.CharacterCard.Show(selectedCharacter);

        //controller.Camera.SwitchToFollowCamera(controller.CurrentCharacter.gameObject.transform);
        controller.Camera.SwitchToOverworldCamera(); // TODO: Replace with follow camera.
    }

    public override void Escape()
    {
        controller.Default();
    }

    void onCharacterStateChange()
    {
        UIManager.Instance.CharacterCard.UpdateStatus(selectedCharacter);
    }

    public override void Destroy()
    {
        // Stop listening to character.
        selectedCharacter.StateContext.onTransition -= onCharacterStateChange;

        // Revert state.
        controller.DisableInteractables();
        UIManager.Instance.SelectedCharacterIcon.Hide();
        UIManager.Instance.CharacterCard.Hide();

        base.Destroy();
    }
}