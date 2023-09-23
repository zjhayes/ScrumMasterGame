using UnityEngine;

public class SelectedCharacterState : GameState
{
    private ContextManager controller;
    private ICharacterController selectedCharacter;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        gameManager.Interactables.EnableInteractables();

        // Listen to character and update status when state changed.
        selectedCharacter = controller.CurrentCharacter;
        selectedCharacter.StateContext.OnTransition += OnCharacterStateChange;

        gameManager.UI.CharacterCard.UpdateCard(selectedCharacter);
        gameManager.UI.CharacterCard.Show();

        gameManager.Camera.SwitchToOverworldCamera();
        base.Handle(controller);
    }

    public override void ChangeView()
    {
        // Enter Scrum Board view.
        controller.SwitchToScrumView();
    }

    public override void OnEscaped()
    {
        controller.Default();
    }

    private void OnCharacterStateChange()
    {
        gameManager.UI.CharacterCard.UpdateStatus(selectedCharacter);
    }

    public override void Exit()
    {
        // Stop listening to character.
        selectedCharacter.StateContext.OnTransition -= OnCharacterStateChange;
        // Revert state.
        gameManager.Interactables.DisableInteractables();
        gameManager.UI.CharacterCard.Hide();

        base.Exit();
    }
}