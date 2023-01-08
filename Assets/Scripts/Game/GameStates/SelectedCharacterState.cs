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
    }

    void onCharacterStateChange()
    {
        UIManager.Instance.CharacterCard.UpdateStatus(controller.CurrentCharacter);
    }

}